using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class coins_manager : MonoBehaviour {
		public AudioClip impact;
		AudioSource audio;
	// Use this for initialization
	void Start () {
				audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		void OnTriggerEnter(Collider other){
				audio.PlayOneShot(impact, 0.7F);
				score_manager.addpoint (1);
				Destroy (gameObject , 0.5f);
		}
}
