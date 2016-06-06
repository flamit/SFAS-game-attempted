using UnityEngine;
using System.Collections;

public class enemy_shooting : MonoBehaviour {
		float timer = 2;
		public Transform bullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if(timer>0){
						timer -= Time.deltaTime;
				}else{
						Instantiate (bullet, transform.position, transform.rotation);	
						timer = 2;
				}

	}
}
