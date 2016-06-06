using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class health_manager : MonoBehaviour {
		
	// Use this for initialization
		public static int health =100000000;
		Text text;
		// Use this for initialization
		void Start () {
				text = GetComponent<Text>(); //get the text component in the gameobject you assigned

		}

		// Update is called once per frame
		void Update () {
				//PlayerPrefs.GetInt ("Score", Score);
				//		guiText.text = /*"Score :"*/""+Score;

				text.text = "health = "+health+"%";



		}
		static public void decrease_health(int points){

				if(health>0){
				health= health -points;
				}else{
						Application.LoadLevel("Gameover");
				}


		}
}
