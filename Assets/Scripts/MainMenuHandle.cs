using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuHandle : MonoBehaviour {

	public GameObject mainMenuCanvas;
	public GameObject instructionCanvas;

	void Start(){
		mainMenuCanvas.SetActive (true);
		instructionCanvas.SetActive (false);
	}

	public void PlayGame(string scene){
		Application.LoadLevel (scene);
	}

	public void EndGame(){
		Application.Quit();
	}

	public void Instructions(){
		mainMenuCanvas.SetActive (false);
		instructionCanvas.SetActive (true);
	}

	public void BackButton(){
		mainMenuCanvas.SetActive (true);
		instructionCanvas.SetActive (false);
	}

}
