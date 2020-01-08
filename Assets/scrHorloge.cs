using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scrHorloge : MonoBehaviour {

	private List<GameObject> playerList;
	private GameObject characterScene;
	private GameObject[] characterPacman;
	private Text timer;
	private float tpsRAZ;
	private float t0;


	public void ajouterPlayer (GameObject p) {
		playerList.Add (p);
	}

	public void supprimerPlayer (GameObject p) {
		if (playerList.Contains (p)) {
			playerList.Remove (p);
		}
	}

	private void RAZ() {
		// Permet aux téléphones de voter a nouveau
		foreach (GameObject o in playerList) {
			o.GetComponent<ControlCharacter>().RAZdejaJoue();
		}

		// Permet aux avatars de se deplacer a nouveau
		foreach (GameObject p in characterPacman) {
			p.GetComponent<scrMove> ().RAZdejaJoue ();
		}
		characterScene.GetComponent<scrMove> ().RAZdejaJoue ();

		t0 = Time.frameCount; // debut d'un nouveau tour
	}

	void Start () {
		playerList = new List<GameObject> ();
		characterScene = GameObject.Find ("Monstre");
		characterPacman = GameObject.FindGameObjectsWithTag ("jpublic");
		timer = GameObject.Find ("Timer").GetComponent<Text> ();

		tpsRAZ = 120;	// on RAZ le dejaJoue de tous les joueurs chaque tpsRAZ frames
		t0 = Time.frameCount;//Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount > t0 + tpsRAZ) {
			RAZ ();
		}

		timer.text = "TIMER\n" + Mathf.RoundToInt ((t0 + tpsRAZ - Time.frameCount) / 15).ToString ();
	}
}
