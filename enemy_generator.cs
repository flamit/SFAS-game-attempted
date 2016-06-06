using UnityEngine;
using System.Collections;

public class enemy_generator : MonoBehaviour {
		public GameObject enemy;
	// Use this for initialization
	void Start () {
				Vector3 pos = new Vector3 (16f , 0, 16f);
				pos.y = 19.7f;
				Instantiate (enemy, pos, Quaternion.identity);
				pos.y = 16.76f;
				Instantiate (enemy, pos, Quaternion.identity);
				pos.y = 13.33f;
				Instantiate (enemy, pos, Quaternion.identity);
				pos.y = 11.04f;
				Instantiate (enemy, pos, Quaternion.identity);
				pos.y = 13.33f;
				Instantiate (enemy, pos, Quaternion.identity);
				pos.y = 8.0f;
				Instantiate (enemy, pos, Quaternion.identity);
				pos.y = 4.54f;
				Instantiate (enemy, pos, Quaternion.identity);
				pos.y = 1.47f;
				Instantiate (enemy, pos, Quaternion.identity);
	}
	
	// Update is called once per frame
	
}
