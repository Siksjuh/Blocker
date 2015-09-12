using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private GameObject[] targetPositions;
	private bool showTargets;
	private GenerateMap layout;
	public GameObject spotMarker;

	// Use this for initialization
	void Start () {
		targetPositions = new GameObject[4];
		showTargets = false;
	}
	
	// Update is called once per frame
	void Update () {
	}


	//When the object is clicked
	void OnMouseDown(){

		layout = GameObject.Find ("GameHandler").GetComponent<GenerateMap> ();

		if (!showTargets) {
			//Checking possible destinations on mouseclick
			for (int i = 0; i < 4; i++) {
				Vector3 positionize = checkEndPoints (i);
				if (positionize != transform.position){
					targetPositions[i] = (GameObject) GameObject.Instantiate (spotMarker, positionize, transform.rotation);
				}
			}
			showTargets = true;
		} else {
			for(int i = 0; i < 4; i++){
				if(targetPositions[i])
					Destroy(targetPositions[i]);
			} showTargets = false;
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
			}
		}

		return retVec;

	}

}
