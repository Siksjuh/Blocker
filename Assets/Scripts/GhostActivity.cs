using UnityEngine;
using System.Collections;

public class GhostActivity : MonoBehaviour {

	public PlayerMovement master;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		master.MoveToPoint (transform.position);
	}

}
