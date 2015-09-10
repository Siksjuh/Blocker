using UnityEngine;
using System.Collections;

public class ComputerMovement : MonoBehaviour {
	public bool ComputerTurn;
	public bool MoveAvailable;
	public bool[] DirectionsAvailable;
	public Vector3[] MovePositions;
	public GameObject PlayerGhost;
	public GameObject ComputerGhost;
	public bool MoveReady;

	public Vector3 pos;
	private int MoveDirection; //0 = Up, 1 = Right, 2 = Down, 3 = Left

	// Use this for initialization
	void Start () {
		MoveAvailable = true;
		ComputerTurn = false;
		DirectionsAvailable = new bool[4];
		MovePositions = new Vector3[4];
	}
	
	// Update is called once per frame
	void Update () {
		if(ComputerTurn == true){
			MoveComputer();
			ComputerTurn=false;
		}
	}

	void MoveComputer(){
		CheckAvailability();
		bool CanMove = false;
		for (int i = 0; i < 4; i++){
			if(DirectionsAvailable[i]==true){
				CanMove = true;
			}
		}
		MoveAvailable = CanMove;

		if(MoveAvailable){
			MoveReady = false;
			do{
				MoveDirection = Random.Range(0,4);
				if(DirectionsAvailable[MoveDirection]==true){
					WaitDone = false;
					StartCoroutine(Waiting(5.0f));

					GameObject[] Ghosts = GameObject.FindGameObjectsWithTag("Ghost");
					if(Ghosts.Length>0){
						for (int i = 0; i < Ghosts.Length; i++){
							Destroy(Ghosts[i]);
						}
					}

					do{
						transform.position = Vector3.Lerp(transform.position, MovePositions[MoveDirection], 1);
					}while(Vector3.Distance(transform.position , MovePositions[MoveDirection])>0);
					MoveReady = true;
					
				}
			}while(!MoveReady);
		}

		/*if(!UpR&&!DownR&&!LeftR&&!RightR){
			CannotMove = true;
		}else{
			bool MoveReady = false;
			do{
				MoveDirection= (int)Random.Range(0.8f,4.2f);
				switch(MoveDirection){
					case 1:
						if(UpR){
							while(UpR){
								transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x,transform.position.y +1.0f,transform.position.z),1);
								CheckAvailability();
							}
							MoveReady=true;
						}
						break;
					case 2:
						if(DownR){
							while(DownR){
								transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x,transform.position.y -1.0f,transform.position.z),1);
								CheckAvailability();
							}
							MoveReady=true;
						}
						break;
					case 3:
						if(LeftR){
							while(LeftR){
								transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x - 1.0f,transform.position.y,transform.position.z),1);
								CheckAvailability();
							}
							MoveReady=true;
						}
						break;
					case 4:
						if(RightR){
							while(RightR){
								transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x +1.0f,transform.position.y,transform.position.z),1);
								CheckAvailability();
							}
							MoveReady=true;
						}
						break;
				}
			}while(MoveReady==false);
		}*/
	}

	void CheckAvailability(){
		Collider[] hitcolliders;

		float step = 1.0f;
		pos = new Vector3(transform.position.x,transform.position.y + step ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);
		if(hitcolliders.Length>0){
			DirectionsAvailable[0] = false;
			MovePositions[0]=Vector3.zero;
		}else {
			DirectionsAvailable[0] = true;
			do{
				MovePositions[0]=pos;
				step += 1.0f;
				pos = new Vector3(transform.position.x,transform.position.y + step ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.4f);
			}while(hitcolliders.Length==0);
			GameObject.Instantiate(ComputerGhost,MovePositions[0],transform.rotation);
			
		}

		step = 1.0f;
		pos = new Vector3(transform.position.x,transform.position.y - step ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);
		if(hitcolliders.Length>0){
			DirectionsAvailable[2] = false;
			MovePositions[2]=Vector3.zero;
		}else {
			DirectionsAvailable[2] = true;
			do{
				MovePositions[2]=pos;
				step += 1.0f;
				pos = new Vector3(transform.position.x,transform.position.y - step ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.4f);
			}while(hitcolliders.Length==0);
			GameObject.Instantiate(ComputerGhost,MovePositions[2],transform.rotation);
			
		}

		step = 1.0f;
		pos = new Vector3(transform.position.x - step,transform.position.y ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);
		if(hitcolliders.Length>0){
			DirectionsAvailable[3] = false;
			MovePositions[3]=Vector3.zero;
		}else {
			DirectionsAvailable[3] = true;
			do{
				MovePositions[3]=pos;
				step += 1.0f;
				pos = new Vector3(transform.position.x - step,transform.position.y ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.4f);
			}while(hitcolliders.Length==0);
			GameObject.Instantiate(ComputerGhost,MovePositions[3],transform.rotation);
			
		}

		step = 1.0f;
		pos = new Vector3(transform.position.x + step,transform.position.y ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);	
		if(hitcolliders.Length>0){
			DirectionsAvailable[1] = false;
			MovePositions[1]=Vector3.zero;
		}else {
			DirectionsAvailable[1] = true;
			do{
				MovePositions[1]=pos;
				step += 1.0f;
				pos = new Vector3(transform.position.x + step,transform.position.y ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.4f);
			}while(hitcolliders.Length==0);
			GameObject.Instantiate(ComputerGhost,MovePositions[1],transform.rotation);
			
		}
	}
}
