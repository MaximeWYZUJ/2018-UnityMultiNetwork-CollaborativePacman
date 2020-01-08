using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCharacter : MonoBehaviour {

	private scrMove character;
	private bool characterAssigned;
	private HFTInput input;
	private GameObject horloge;
	private HFTGamepad gp;
	private bool dejaJoue;
	private bool isJoueurScene;


	private void inscrireHorloge() {
		horloge.GetComponent<scrHorloge> ().ajouterPlayer (this.gameObject);
	}

	private void associerCharacter () {
		GameObject[] listePacman;

		if (isJoueurScene) {
			gp.Color = Color.yellow;
			character = GameObject.Find ("Monstre").GetComponent<scrMove> ();;
		} else {
			listePacman = GameObject.FindGameObjectsWithTag ("jpublic");
			GameObject pacmanMinPlayer = null;
			foreach (GameObject pacman in listePacman) {
				if (pacmanMinPlayer == null) {pacmanMinPlayer = pacman;}
				else {
					if (pacman.GetComponent<scrMove>().nbPlayerControllers < pacmanMinPlayer.GetComponent<scrMove>().nbPlayerControllers) {
						pacmanMinPlayer = pacman;
					}
				}
			}
			gp.Color = pacmanMinPlayer.GetComponent<SpriteRenderer> ().color;
			character = pacmanMinPlayer.GetComponent<scrMove>();
			character.nbPlayerControllers += 1;
		}
	}

	public void RAZdejaJoue () {
		dejaJoue = false;
        gp.SendResetPad();
    }


	void Start () {
		gp = GetComponent<HFTGamepad> ();
		dejaJoue = false;
		horloge = GameObject.Find ("Horloge");
		characterAssigned = false;

		input = GetComponent<HFTInput> ();

		inscrireHorloge ();
	}
	

	void Update () {
		if (!characterAssigned && !string.Equals (gp.Name, "nope")) { // on laisse le joueur choisir son nom avant de lui assigner un avatar
			characterAssigned = true;
			isJoueurScene = string.Equals (gp.Name, "jsceneup") || string.Equals (gp.Name, "jscenedown")
				|| string.Equals (gp.Name, "jsceneleft") || string.Equals (gp.Name, "jsceneright");
			associerCharacter ();
		}

		if (characterAssigned && !dejaJoue) {
			if (input.GetAxis ("Horizontal") > 0) {
				if (!isJoueurScene || string.Equals (gp.Name, "jsceneright")) {
					character.moveRight ();
					dejaJoue = true;
				}
			} else if (input.GetAxis ("Horizontal") < 0) {
				if (!isJoueurScene || string.Equals (gp.Name, "jsceneleft")) {
					character.moveLeft ();
					dejaJoue = true;
				}
			} else if (input.GetAxis ("Vertical") < 0) { // axe vertical inverse sur le controller
				if (!isJoueurScene || string.Equals (gp.Name, "jsceneup")) {
					character.moveUp ();
					dejaJoue = true;
				}
			} else if (input.GetAxis ("Vertical") > 0) {
				if (!isJoueurScene || string.Equals (gp.Name, "jscenedown")) {
					character.moveDown ();
					dejaJoue = true;
				}
			}
		}
	}
}
