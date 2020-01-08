using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstreSndManager : MonoBehaviour {

	public AudioClip sndExplosion;
	public AudioClip sndCoin;

	private AudioSource asrc;

	void Start () {
		asrc = GetComponent<AudioSource> ();
	}
	

	void OnTriggerEnter2D (Collider2D other) {
		if (string.Equals (other.gameObject.tag, "jpublic")) {
			asrc.clip = sndExplosion;
			asrc.Play ();
		}

		if (string.Equals (other.gameObject.tag, "coin")) {
			if (!asrc.isPlaying) {
				asrc.clip = sndCoin;
				asrc.Play ();
			}
		}
	}
}
