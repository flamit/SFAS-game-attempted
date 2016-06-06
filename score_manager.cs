using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class score_manager : MonoBehaviour {

	static int Score =0;
		Text text;
	// Use this for initialization
	void Start () {
		 text = GetComponent<Text>(); 

	}

	// Update is called once per frame
	void Update () {


				text.text = "score = "+Score;



	}
	static public void addpoint(int points){

		Score= Score +points;
				if(Score == 20){
						Application.LoadLevel("Play_again");
				}
	}

}
