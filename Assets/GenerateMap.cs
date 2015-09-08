using UnityEngine;
using System.Collections;

public class GenerateMap : MonoBehaviour {

public GameObject CubeB;
public GameObject CubeW;
public GameObject Player;
public GameObject Computer;
public Vector3 pos;
public Vector3[] positions;
public bool ready;



	// Use this for initialization
	void Start () {
		positions = new Vector3[4];
		for (int i=-7;i<=7;i++){
			for (int j=-7;j<=7;j++){
				pos=new Vector3(j,i,0);
				if((i+j)%2==0){
						GameObject.Instantiate(CubeB, pos ,transform.rotation);
				}else{
						GameObject.Instantiate(CubeW, pos , transform.rotation);
				}
			}
		}
		
		positions[0] = GeneratePosition();
		GameObject.Instantiate(Computer,positions[0],transform.rotation);
		for (int k=1;k<4;k++){
			do 
			{
				ready = true;
				positions[k] = GeneratePosition();
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
	
	Vector3 GeneratePosition(){
		return new Vector3((int)Random.Range(-7, 7),(int)Random.Range(-7,7),-1);
	}

	//GameObject.Instantiate(Point, pos, transform.rotation);

}
