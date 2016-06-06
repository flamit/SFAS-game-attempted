using UnityEngine;
using System.Collections;

public class Load_level : MonoBehaviour {

	// Use this for initialization
		public GameObject UI;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		void OnMouseDown(){
				Debug.Log ("now");
				UI.SetActive (false);
				int level = (int)Random.Range (1, 9);
				Application.LoadLevel(level);

	}
}
