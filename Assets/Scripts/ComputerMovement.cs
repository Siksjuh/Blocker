using UnityEngine;
using System.Collections;

public class ComputerMovement : MonoBehaviour {
	public bool ComputerTurn;
	public static bool CannotMove;

	public bool UpR;
	public bool DownR;
	public bool LeftR;
	public bool RightR;
	public Vector3 pos;
	private int MoveDirection;

	// Use this for initialization
	void Start () {
		CannotMove = false;
		ComputerTurn = false;
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
		if(!UpR&&!DownR&&!LeftR&&!RightR){
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
		}
	}

	void CheckAvailability(){
		Collider[] hitcolliders;

		pos = new Vector3(transform.position.x,transform.position.y + 1.0f ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);
		if(hitcolliders.Length>0){
			UpR = false;
		}else {
			UpR = true;
		}
		
		pos = new Vector3(transform.position.x,transform.position.y - 1.0f ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);
		if(hitcolliders.Length>0){ 
			DownR = false;
		}
		else {
			DownR = true;
		}

		pos = new Vector3(transform.position.x - 1.0f,transform.position.y ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);
		if(hitcolliders.Length>0) {
			LeftR = false;
		}
		else {
			LeftR = true;
		}

		pos = new Vector3(transform.position.x + 1.0f,transform.position.y ,transform.position.z);
		hitcolliders = Physics.OverlapSphere(pos, 0.4f);	
		if(hitcolliders.Length>0) {
			RightR = false;
		}
		else {
			RightR = true;
		}
	}
}
