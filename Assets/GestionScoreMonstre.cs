using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionScoreMonstre : MonoBehaviour {

	public GameObject scoreTxt;

	private int score;
	private Text txt;

	// Use this for initialization
	void Start () {
		score = 0;
		txt = scoreTxt.GetComponent<Text> ();
		txt.text = score.ToString();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (string.Equals (other.gameObject.tag, "mur")) {
			score -= 20;
		} else if (string.Equals (other.gameObject.name, "Pacman1") || string.Equals (other.gameObject.name, "Pacman2")) {
			score -= 200;
		} else if (string.Equals (other.gameObject.tag, "coin")) {
			score += 40;
			Destroy (other.gameObject);
		}

		txt.text = score.ToString();
	}
}
