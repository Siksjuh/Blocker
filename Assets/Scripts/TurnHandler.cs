using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnHandler : MonoBehaviour {
	public static bool PlayerTurnComplete;
	public static bool ComputerTurnComplete;
	public bool PlayerToggle;

	public GameObject TurnCanvas;
	public Text TurnText;

	private float Timer;
	// Use this for initialization
	void Start () {
		PlayerToggle = true;
		Timer = 0.0f;	
		TurnCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerTurnComplete){
			PlayerTurnComplete=false;
			TurnText.text="Computer's turn!";
			TurnCanvas.SetActive(true);
			Timer = 0.0f;
			PlayerToggle=false;
		}
		if(ComputerTurnComplete){
			ComputerTurnComplete = false;
			TurnText.text="Your turn!";
			TurnCanvas.SetActive(true);
			Timer = 0.0f;
			PlayerToggle = true;
		}
		Timer += Time.deltaTime;
		if(Timer>2){
			TurnCanvas.SetActive(false);
			if(PlayerToggle){
				PlayerTurn();
			}else{
				ComputerMovement.ComputerTurn=true;
			}
		}
	}

	void PlayerTurn(){
		PlayerTurnComplete = true;
	}
}
