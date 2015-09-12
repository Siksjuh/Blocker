using UnityEngine;
using System.Collections;

public class GenerateMap : MonoBehaviour {

public GameObject CubeB;
public GameObject CubeW;
public GameObject Border;
public GameObject Player;
public GameObject Computer;
public GameObject WallVertical;
public GameObject WallHorizontal;
public Vector3 pos;
public Vector3[] positions;
public bool ready;
public Vector3[] verticalWalls;
public Vector3[] horizontalWalls;



	// Use this for initialization
	void Start () {
		positions = new Vector3[4];

		//Generate The board. 15x15 board, borders around, checkerboard style
		for (int i=-8;i<=8;i++){
			for (int j=-8;j<=8;j++){
				pos=new Vector3(j,i,0);
				if(Mathf.Abs(i)==8||Mathf.Abs(j)==8){
					GameObject.Instantiate(Border, pos ,transform.rotation);
				}else{
					if((i+j)%2==0){
							GameObject.Instantiate(CubeB, pos ,transform.rotation);
					}else{
							GameObject.Instantiate(CubeW, pos , transform.rotation);
					}
				}
			}
		}


		generateWalls ();


		//Generate random positions for Computer and Player objects.
		positions[0] = GeneratePosition(0);
		GameObject.Instantiate(Computer,positions[0],transform.rotation);
		for (int k=1;k<4;k++){
			do 
			{
				ready = true;
				positions[k] = GeneratePosition(0);
				//Check if the space is occupied
				for (int l=0;l<k;l++){
					if(positions[k]==positions[l]){
						ready=false;
					}
				}
			}while(!ready);
			
			GameObject.Instantiate(Player,positions[k],transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	//Generates random position on the board
	Vector3 GeneratePosition(int wallPos){
		Vector3 retVec = Vector3.zero;
		if (wallPos != 0) {
			if(wallPos == 1)
				retVec = new Vector3 (.5f + (int)Random.Range (-7, 7), (int)Random.Range (-7, 8), -1);
			if(wallPos == 2)
				retVec = new Vector3 ((int)Random.Range (-7, 8), .5f +(int)Random.Range (-7, 7), -1);
		} else {
			retVec = new Vector3 ((int)Random.Range (-7, 7), (int)Random.Range (-7, 7), -1);
		}
		return retVec;
	}



	void generateWalls(){

		verticalWalls = new Vector3[27];
		horizontalWalls = new Vector3[27];

		for(int i = 0; i < 27; i++){
		AgainV:
			verticalWalls[i] = GeneratePosition(1);
			for(int j = 0; j < i; j++){
				if(verticalWalls[j] == verticalWalls[i]){
					goto AgainV;
				}
			}

		AgainH:
				horizontalWalls[i] = GeneratePosition(2);
			for(int j = 0; j < i; j++){
				if(horizontalWalls[j] == horizontalWalls[i]){
					goto AgainH;
				}
			}

			GameObject.Instantiate(WallVertical, verticalWalls[i], transform.rotation);
			GameObject.Instantiate(WallHorizontal, horizontalWalls[i], transform.rotation);
		}

	}


}
