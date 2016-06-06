using UnityEngine;
using System.Collections;

public class bonus_generator : MonoBehaviour {
		Vector3 pos = new Vector3 (0 , 0, -10);
		public Transform bonus;
	// Use this for initialization
	void Start () {
				for(int i=0 ; i<36 ; i++){
						pos.x = Random.Range (1.9f, 27.7f);
						pos.y = 19.7f;
						pos.z = Random.Range (5.4f, 28);
						if(i == 5){
								pos.y = 16.76f;		
						}
						if(i == 10){
								pos.y = 13.33f;		
						}
						if(i == 15){
								pos.y = 11.04f;		
						}
						if(i == 20){
								pos.y = 13.33f;		
						}
						if(i == 25){
								pos.y = 8.0f;;		
						}
						if(i == 30){
								pos.y =4.54f;	
						}
						if(i == 35){
								pos.y =  1.47f;	
						}
						Instantiate (bonus, pos, Quaternion.identity);
				}
	}
	
	// Update is called once per frame
	/*void Update () {
			
		}*/
}
