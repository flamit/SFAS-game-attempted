using UnityEngine;
using System.Collections;

public class gameOver : MonoBehaviour {
		public GameObject GameOver;
	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
			showOver();
	}
		void showOver(){
				if(health_manager.health<=0){
						Application.LoadLevel("Gameover");
				}
		}
}
