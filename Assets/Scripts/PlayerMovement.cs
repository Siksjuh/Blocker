using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {

	private GameObject[] targetPositions;
	private bool showTargets;
	private GenerateMap layout;
	public GameObject spotMarker;
	private Vector3 moveTo;
	private Vector3 moveFrom;
	private float timer;
	private TurnHandler turnSignaler;
	private bool computerTurn;
	private bool moving;


	// Use this for initialization
	void Start () {
		layout = GameObject.Find ("GameHandler").GetComponent<GenerateMap> ();
		turnSignaler = GameObject.Find ("GameHandler").GetComponent<TurnHandler> ();
		targetPositions = new GameObject[0];
		layout.showingTargets = false;
		moveTo = transform.position;
		timer = 0;
		computerTurn = true;
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		showTargets = layout.showingTargets;
		if (moveTo != transform.position) {
			timer += Time.deltaTime;
			transform.position = Vector3.Lerp(moveFrom, moveTo, timer);
			if(Vector3.Distance(transform.position, moveTo) < .2f){
				transform.position = moveTo;
				computerTurn = true;
				moving = false;
				turnSignaler.PlayerTurnComplete = computerTurn;
			}
		} else {
			timer = 0;
			moveFrom = transform.position;
		} if (computerTurn)
			computerTurn = turnSignaler.PlayerActive;
	}


	//When the object is clicked
	void OnMouseDown(){
		
		if (!computerTurn && !moving) {
			if (!showTargets) {
				targetPositions = new GameObject[4];
				//Checking possible destinations on mouseclick
				for (int i = 0; i < 4; i++) {
					Vector3 positionize = checkEndPoints (i);
					if (positionize != transform.position) {
						targetPositions [i] = (GameObject)GameObject.Instantiate (spotMarker, positionize, transform.rotation);
						targetPositions [i].GetComponent<GhostActivity> ().master = this;
					}
				}
				layout.showingTargets = true;
			} else if (targetPositions.Length != 0) {
				DestroyGhosts ();
			}
		}
	}



	//Check a possible location.
	Vector3 checkEndPoints(int dir){
		Vector3 retVec = transform.position;
		
		//Check up.
		if (dir == 0){
			retVec.y = 7;
			for (int i = 0; i < layout.horizontalWalls.Length; i++) {
				Vector3 test = layout.horizontalWalls[i];
				if (test.y > transform.position.y && test.x == retVec.x && test.y < retVec.y) {
					retVec.y = test.y -.5f;
				}
			} for (int i = 0; i < layout.positions.Length; i++){
				Vector3 test = layout.positions[i].transform.position;
				if(test.x == retVec.x && test.y > transform.position.y && test.y < retVec.y)
					retVec.y = test.y-1;
			}
		}

		//Check down.
		if (dir == 1){
			retVec.y = -7;
			for (int i = 0; i < layout.horizontalWalls.Length; i++) {
				Vector3 test = layout.horizontalWalls[i];
				if (test.y < transform.position.y && test.x == retVec.x && test.y > retVec.y) {
					retVec.y = test.y +.5f;
				}
			}for (int i = 0; i < layout.positions.Length; i++){
				Vector3 test = layout.positions[i].transform.position;
				if(test.x == retVec.x && test.y < transform.position.y && test.y > retVec.y)
					retVec.y = test.y+1;
			}
		}

		//Check right.
		if (dir == 2){
			retVec.x = 7;
			for (int i = 0; i < layout.verticalWalls.Length; i++) {
				Vector3 test = layout.verticalWalls[i];
				if (test.x > transform.position.x && test.y == retVec.y && test.x < retVec.x) {
					retVec.x = test.x -.5f;
				}
			}for (int i = 0; i < layout.positions.Length; i++){
				Vector3 test = layout.positions[i].transform.position;
				if(test.y == retVec.y && test.x > transform.position.x && test.x < retVec.x)
					retVec.x = test.x-1;
			}
		}

		//Check left.
		if (dir == 3){
			retVec.x = -7;
			for (int i = 0; i < layout.verticalWalls.Length; i++) {
				Vector3 test = layout.verticalWalls[i];
				if (test.x < transform.position.x && test.y == retVec.y && test.x > retVec.x) {
					retVec.x = test.x +.5f;
				}
			}for (int i = 0; i < layout.positions.Length; i++){
				Vector3 test = layout.positions[i].transform.position;
				if(test.y == retVec.y && test.x < transform.position.x && test.x > retVec.x)
					retVec.x = test.x+1;
			}
		}

		return retVec;

	}

	public void MoveToPoint (Vector3 to){
		DestroyGhosts ();
		moveTo = to;
		moving = true;
	}


	public void DestroyGhosts(){
			for(int i = 0; i < 4; i++){
				if(targetPositions[i])
					Destroy(targetPositions[i]);
			} targetPositions = new GameObject[0];
			layout.showingTargets = false;
		}


}
