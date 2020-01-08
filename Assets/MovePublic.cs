using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePublic : MonoBehaviour, scrMove {

	public float taille_case;
	private int nbPlayerControl; // c'est le nombre de joueurs (co ou deco) assignés à cet objet
	private int choixHor;
	private int choixVer;
	private Transform tr;
	private bool dejaJoue;
	private float tpsRAZ;
	private int direction;	// 0: right, 1: up, 2: left, 3: down

	private float nextx;	// coo du point ou on veut aller, permet de se "recentrer"
	private float nexty;
	private float oldx;		// coo du point ou on etait avant de commencer a bouger, permet d'y retourner en cas de collision
	private float oldy;
	private bool collision;

	private int bufferTP;


	public int nbPlayerControllers {
		get {
			return nbPlayerControl;
		}
		set {
			nbPlayerControl = value;
		}
	}

	// ACCESSEUR DES VOTES DU PUBLIC
	public int voteDroit {
		get {
			if (choixHor > 0) {
				return choixHor;
			} else
				return 0;
		}
	}

	public int voteGauche {
		get {
			if (choixHor < 0) {
				return -choixHor;
			} else
				return 0;
		}
	}

	public int voteHaut {
		get {
			if (choixVer > 0) {
				return choixVer;
			} else
				return 0;
		}
	}

	public int voteBas {
		get {
			if (choixVer < 0) {
				return -choixVer;
			} else
				return 0;
		}
	}


	// DEPLACEMENTS ET UPDATE DU VOTE
	public void moveLeft() {
		choixHor -= 1;
	}

	public void moveRight() {
		choixHor += 1;
	}

	public void moveUp() {
		choixVer += 1;
	}

	public void moveDown() {
		choixVer -= 1;
	}

	public void move() { // appele a chaque tic d'horloge
		if (Mathf.Abs (choixHor) > Mathf.Abs (choixVer)) {	// on se déplace horizontalement
			if (choixHor > 0) {								// on va vers la droite
				direction = 0;//tr.Translate (new Vector3 (taille_case, 0, 0));
				dejaJoue = true;
				nextx = tr.position.x + taille_case;
			} else if (choixHor < 0) {						// on va vers la gauche
				direction = 2;//tr.Translate (new Vector3 (-taille_case, 0, 0));
				dejaJoue = true;
				nextx = tr.position.x - taille_case;
			}
		} else { 											// on se déplace verticalement
			if (choixVer > 0) {								// on va vers le haut
				direction = 1;//tr.Translate (new Vector3 (0, taille_case, 0));
				dejaJoue = true;
				nexty = tr.position.y + taille_case;
			} else if (choixVer < 0) {						// on va vers le bas
				direction = 3;//tr.Translate (new Vector3 (0, -taille_case, 0));
				dejaJoue = true;
				nexty = tr.position.y - taille_case;
			} else { // choixHor = choixVer = 0
				direction = -1;
				dejaJoue = true;
				// pas de maj de nextx nexty car on ne se deplace pas
			}
		}
	}

	public void RAZdejaJoue() {
		if (!collision) {
			tr.position = new Vector3(nextx, nexty, 0); // on corrige le léger decalage induit par le mouvement continu
			oldx = nextx;
			oldy = nexty;
		}
		move ();

		dejaJoue = false;
		collision = false;
		choixVer = 0;
		choixHor = 0;
	}


	void Start () {
		tr = this.GetComponent<Transform> ();
		choixHor = 0;
		choixVer = 0;
		dejaJoue = false;
		tpsRAZ = 120; // faire attention a avoir le meme tpsRAZ que dans l'horloge, pour rester synchronise
		direction = -1;
		collision = false;

		oldx = tr.position.x;
		oldy = tr.position.y;
		nextx = tr.position.x;
		nexty = tr.position.y;

		bufferTP = 0;
	}

	void Update () {
		switch (direction) {
		case 0: // on va a droite
			tr.Translate (new Vector3 (taille_case / tpsRAZ, 0, 0));
			break;
		case 1: // on va en haut
			tr.Translate (new Vector3 (0, taille_case / tpsRAZ, 0));
			break;
		case 2: // on va a gauche
			tr.Translate (new Vector3 (-taille_case / tpsRAZ, 0, 0));
			break;
		case 3: // on va en bas
			tr.Translate (new Vector3 (0, -taille_case / tpsRAZ, 0));
			break;
		}
	}


	void OnTriggerEnter2D(Collider2D other) {

		if (string.Equals (other.gameObject.tag, "mur")) {
			tr.position = new Vector3 (oldx, oldy, 0);
			nextx = oldx;
			nexty = oldy;
			direction = -1;
			collision = true;
		}

		if (string.Equals (other.gameObject.tag, "teleport")) {
			direction = -1;

			nextx = tr.position.x;
			nexty = tr.position.y;
			oldx = nextx;
			oldy = nexty;
		}
	}


	void OnTriggerStay2D (Collider2D other) {
		
		if (string.Equals (other.gameObject.tag, "teleport")) {
			Debug.Log ("collision tp");
			if (tr.position.x > 3 && direction == 0) {			// on est à droite donc on se tp a gauche
				tr.position = new Vector3 (-6f, 0f, 0f);
			} else if (tr.position.y > 3 && direction == 1) {	// on est en haut donc on se tp en bas
				tr.position = new Vector3 (0f, -6f, 0f);
			} else if (tr.position.x < 3 && direction == 2) {	// on est a gauche donc on se tp a droite
				tr.position = new Vector3 (6f, 0f, 0f);
			} else if (tr.position.y < 3 && direction == 3) {	// on est en bas donc on se tp en haut
				tr.position = new Vector3 (0f, 6f, 0f);
			}
		}
	}
}
