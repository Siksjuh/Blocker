using UnityEngine;
using System.Collections;

public class ComputerMovement : MonoBehaviour {
	//public bool ComputerTurn; //temporary variable for testing
	public bool MoveAvailable;
	public bool[] DirectionsAvailable;
	public Vector3[] MovePositions;
	public GameObject ComputerGhost;
	public bool MoveReady;
	public float Timer;
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
			Timer+=Time.deltaTime;
			//3s delay before computer moves. Simulates "thinking".
			if(Timer>3.0f){
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

		//Check if computer can move
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
				//get random direction 0-3, 0) Up, 1) Right, 2) Down, 3) Left
				MoveDirection = Random.Range(0,4);
				//if the computer can't move to that direction, a new direction is selected.
				if(DirectionsAvailable[MoveDirection]==true){
					MoveReady = true;				
				}
			}while(!MoveReady);
		}
		ComputerActive = true;
		Timer=0.0f;
	}

	void CheckAvailability(){  //check for available movement directions
		Collider[] hitcolliders;
		//check Up direction
		float step = 1.0f;
		pos = new Vector3(transform.position.x,transform.position.y + step ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);
		//If next space is blocked, this direction isn't available
		if(hitcolliders.Length>0){
			DirectionsAvailable[0] = false;
			MovePositions[0]=Vector3.zero;
		}else {
			//Direction is available, so we need to find the last available spot in this direction
			DirectionsAvailable[0] = true;
			do{
				MovePositions[0]=pos;
				step += 1.0f;
				pos = new Vector3(transform.position.x,transform.position.y + step ,transform.position.z);
				hitcolliders = Physics.OverlapSphere(pos, 0.4f);
			}while(hitcolliders.Length==0);
			//Generate a ghost-object in this position for clarity.
			GameObject.Instantiate(ComputerGhost,MovePositions[0],transform.rotation);
			
		}
		//Check Down direction
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
		//Check Left Direction
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
		//Check Right Direction
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
		/* TODO:
		void FindBestMoves(){

		}*/
	}
}
