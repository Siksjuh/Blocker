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

	// Use this for initialization
	void Start () {
		MoveAvailable = true;
		ComputerActive = false;
		DirectionsAvailable = new bool[4];
		MovePositions = new Vector3[4];
	}
	
	// Update is called once per frame
	void Update () {
		if (ComputerTurn == true && ComputerActive == false){
			ComputerTurn = false;
			CalculateMove();
		}
		if(ComputerActive==true){
			CompTimer+=Time.deltaTime;
			//3s delay before computer moves. Simulates "thinking".
			if(CompTimer>3.0f){
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

		// TODO: FindBestMoves(); 

		if(MoveAvailable){
			MoveReady = false;
			do{
				//get random direction 0-3, 0) Up, 1) Right, 2) Down, 3) Left
				MoveDirection = Random.Range(0,4);
				//if the computer can't move to that direction, a new direction is selected.
				if(DirectionsAvailable[MoveDirection]==true){
					MoveReady = true;				
				}
			}while(!MoveReady);
		}else{
			Time.timeScale=0;
		}
		ComputerActive = true;
		CompTimer=0.0f;
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
			GameObject.Instantiate(ComputerGhost,MovePositions[0],transform.rotation);
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
			GameObject.Instantiate(ComputerGhost,MovePositions[2],transform.rotation);
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
			GameObject.Instantiate(ComputerGhost,MovePositions[3],transform.rotation);
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
		GameObject.Instantiate(ComputerGhost,MovePositions[1],transform.rotation);}
		
		bool temp = false;
		for(int i = 0; i<4;i++){
			if (MovePositions[i]==Vector3.zero){
				DirectionsAvailable[i]=false;
			}else{
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
