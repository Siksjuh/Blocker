using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnHandler : MonoBehaviour {
	public bool PlayerTurnComplete;
	public static bool ComputerTurnComplete;
	public bool PlayerActive;
	public bool ComputerActive;

	public GameObject TurnCanvas;
	public Text TurnText;
	//public GameObject PlayerCanvas;

	private float Timer;
	// Use this for initialization
	void Start () {
		Timer = 0.0f;	
		TurnCanvas.SetActive(false);
		PlayerTurnComplete = true;
		ComputerTurnComplete = false;
		//PlayerCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerTurnComplete){
			TurnText.text = "Computer's turn.";
			TurnCanvas.SetActive(true);
			PlayerTurnComplete = false;
			PlayerActive = false;
			ComputerActive = true;
			ComputerTurn();
			Timer = 0.0f;

		}
		if(ComputerTurnComplete){
			TurnText.text = "Your turn.";
			TurnCanvas.SetActive(true);
			ComputerTurnComplete = false;
			ComputerActive = false;
			PlayerActive = true;
			PlayerTurn();
			Timer = 0.0f;
		}

		Timer += Time.deltaTime;
		if(Timer>2){
			TurnCanvas.SetActive(false);
			
		}
	}
	void PlayerTurn(){
		//PlayerCanvas.SetActive(true);
	}
	void ComputerTurn(){
		ComputerMovement.ComputerTurn=true;
	}

	/*
	public void ButtonClick(){
		PlayerCanvas.SetActive(false);
		PlayerTurnComplete = true;
	}*/
}
