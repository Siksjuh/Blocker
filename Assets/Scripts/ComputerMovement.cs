using UnityEngine;
using System.Collections;

public class ComputerMovement : MonoBehaviour {
	//public bool ComputerTurn; //temporary variable for testing
	public bool MoveAvailable;
	public bool[] DirectionsAvailable;
	public Vector3[] MovePositions;
	public GameObject ComputerGhost;
	public bool MoveReady;
	public float CompTimer;
	public Vector3 pos;
	private int MoveDirection; //0 = Up, 1 = Right, 2 = Down, 3 = Left
	public bool ComputerActive;
	public static bool ComputerTurn;
	public int MoveCounter;
	public float ThinkTime;
	public int[] MoveValue;

	// Use this for initialization
	void Start () {
		MoveAvailable = true;
		ComputerActive = false;
		DirectionsAvailable = new bool[4];
		MovePositions = new Vector3[4];
		MoveValue = new int[4];
	}
	
	// Update is called once per frame
	void Update () {
		
		if (ComputerTurn == true){
			ComputerTurn = false;
			CalculateMove();
			if(MoveCounter<2){
				ThinkTime=1.0f;
			}else{
				ThinkTime=Random.Range(2.0f,5.0f);
			}
		}

		
		if(ComputerActive==true){
			CompTimer+=Time.deltaTime;
			//3s delay before computer moves. Simulates "thinking".
			if(CompTimer>ThinkTime){
				//remove all ghost objects
				DestroyGhosts();
				transform.position = Vector3.Lerp(transform.position, MovePositions[MoveDirection], Time.deltaTime*5);
				if(Vector3.Distance(transform.position,MovePositions[MoveDirection])<0.1f){
					//Comparison isn't accurate, so we make sure that Computer stops at the exact location
					transform.position = MovePositions[MoveDirection];
					//End Computer turn
					ComputerActive=false;
					TurnHandler.ComputerTurnComplete = true;
				}
			}
		}
	}
	//Destroy All Ghost-gameobjects
	void DestroyGhosts(){
		GameObject[] Ghosts = GameObject.FindGameObjectsWithTag("Ghost");
			if(Ghosts.Length>0){
				for (int i = 0; i < Ghosts.Length; i++){
					Destroy(Ghosts[i]);
				}
			}
	}

	void CalculateMove(){
		CheckAvailability();

		// TODO: 
		if(MoveAvailable){
			FindBestMoves(); 

			MoveReady = false;
			do{
				int MaxValue = 0; 
				int MaxIndex = 0;
				for(int i = 0; i < 4; i++){
					if(MoveValue[i]>MaxValue){
						MaxValue = MoveValue[i];
						MaxIndex = i;
					}else if(MoveValue[i]==MaxValue){
						int temp = Random.Range(0,1);
						if(temp==0) {
							MaxValue = MoveValue[i];
							MaxIndex = i;
						}
					}
				}
				MoveDirection = MaxIndex;
				MoveReady = true;
				//get random direction 0-3, 0) Up, 1) Right, 2) Down, 3) Left
				//MoveDirection = Random.Range(0,4);
				//if the computer can't move to that direction, a new direction is selected.
			}while(!MoveReady);
		}else{
			Time.timeScale=0;
		}
		ComputerActive = true;
		CompTimer=0.0f;
	}

	void FindBestMoves(){
		Collider[] hitcolliders;
		for(int i = 0; i < 4; i++){
			MoveValue[i] = 100;
			if(DirectionsAvailable[i]==true){
				hitcolliders = Physics.OverlapSphere(MovePositions[i],0.5f);
				if(hitcolliders.Length>=3){
					MoveValue[i] -= 60;
				}else if(hitcolliders.Length==2){
					MoveValue[i] -= 30;
				}
			GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
			for(int j = 0; j<Players.Length;j++){
				int start, stop;
				bool Obstacles = false;
				if(Players[j].transform.position.y == MovePositions[i].y){
					if(Players[j].transform.position.x>MovePositions[i].x){
						start = (int)MovePositions[i].x;
						stop = (int)Players[j].transform.position.x;
					}else{
						stop = (int)MovePositions[i].x;
						start = (int)Players[j].transform.position.x;
					}
					for(float k = start;k<stop;k+=0.5f){
						hitcolliders = Physics.OverlapSphere(new Vector3(k, MovePositions[i].y, -1),0.05f);
						if(hitcolliders.Length>1){
							Obstacles = true;
						}else if(hitcolliders.Length==1){
							GameObject go = hitcolliders[0].gameObject;
							if(go.tag != "Computer"){
								Obstacles = true;
								Debug.Log("sadasd");
							}
						}
						for (int x= 0;x< hitcolliders.Length;x++){
							if (hitcolliders[x].gameObject.tag == "Computer") Debug.Log("Computer found");
						}
					}

				}else if(Players[j].transform.position.x==MovePositions[i].x){
					if(Players[j].transform.position.y>MovePositions[i].y){
						start = (int)MovePositions[i].y;
						stop = (int)Players[j].transform.position.y;
					}else{
						stop = (int)MovePositions[i].y;
						start = (int)Players[j].transform.position.y;
					}
					for(float k = start;k<stop;k+=0.5f){
						hitcolliders = Physics.OverlapSphere(new Vector3(MovePositions[i].x, k, -1),0.05f);
						if(hitcolliders.Length>1){
							Obstacles = true;
						}else if(hitcolliders.Length==1){
							GameObject go = hitcolliders[0].gameObject;
							if(go.tag != "Computer"){
								Obstacles = true;
								Debug.Log("Asdasda");
							}
						}
					}
				}

				if(Obstacles == true){
					MoveValue[i] -= 20;
				}
			}
			}else{
				MoveValue[i]=0;
			}
		}
	}

	void CheckAvailability(){  //check for available movement directions
		Collider[] hitcolliders;
		//check Up direction
		float step = 0.5f;
		pos = new Vector3(transform.position.x,transform.position.y + step ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.05f);
		MovePositions[0]=Vector3.zero;
	
		if(hitcolliders.Length == 0){
			do{
				if(step%1==0){
					MovePositions[0]=pos;
				}
				step += 0.5f;
				pos = new Vector3(transform.position.x,transform.position.y + step ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.1f);
			}while(hitcolliders.Length==0);
			//Generate a ghost-object in this position for clarity.
			GameObject Ghost = Instantiate(ComputerGhost,MovePositions[0],transform.rotation)as GameObject;
			Ghost.name = "GhostUp";
		}

		//Check Down direction
		step = 0.5f;
		pos = new Vector3(transform.position.x,transform.position.y - step ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.05f);
		MovePositions[2]=Vector3.zero;
		if(hitcolliders.Length == 0){
			do{
				if(step%1==0){
					MovePositions[2]=pos;
				}
				step += 0.5f;
				pos = new Vector3(transform.position.x,transform.position.y - step ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.1f);
			}while(hitcolliders.Length==0);
			GameObject Ghost = Instantiate(ComputerGhost,MovePositions[2],transform.rotation)as GameObject;
			Ghost.name = "GhostDown";
		}

		//Check Left Direction
		step = 0.5f;
		pos = new Vector3(transform.position.x - step,transform.position.y ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.05f);
		MovePositions[3]=Vector3.zero;
		
		if(hitcolliders.Length == 0){
			do{
				if(step%1==0){
					MovePositions[3]=pos;
				}
				step += 0.5f;
				pos = new Vector3(transform.position.x - step,transform.position.y ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.1f);
			}while(hitcolliders.Length==0);
			GameObject Ghost = Instantiate(ComputerGhost,MovePositions[3],transform.rotation)as GameObject;
			Ghost.name = "GhostLeft";
		}
		
		//Check Right Direction
		step = 0.5f;
		pos = new Vector3(transform.position.x + step,transform.position.y ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.05f);
		MovePositions[1]=Vector3.zero;
		if(hitcolliders.Length == 0){
			do{
				if(step%1==0){
					MovePositions[1]=pos;
				}
				step += 0.5f;
				pos = new Vector3(transform.position.x + step,transform.position.y ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.1f);
			}while(hitcolliders.Length==0);
		GameObject Ghost = Instantiate(ComputerGhost,MovePositions[1],transform.rotation) as GameObject;
		Ghost.name = "GhostRight";
	}
		

		bool temp = false;
		MoveCounter = 0;
		for(int i = 0; i<4;i++){
			if (MovePositions[i]==Vector3.zero){
				DirectionsAvailable[i]=false;
			}else{
				MoveCounter ++;
				DirectionsAvailable[i]=true;
				temp = true;
			}
		}
		MoveAvailable = temp;
		/* TODO:
		void FindBestMoves(){

		}*/
	}
}
