using UnityEngine;
using System.Collections;

public class hit_player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		void OnTriggerEnter(Collider other){
				health_manager.decrease_health (10);
				Destroy (gameObject);

		}
}
