using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour, scrMove {

	public float taille_case;
	public GameObject particule;
	public Sprite sprUp;
	public Sprite sprDown;
	public Sprite sprLeft;
	public Sprite sprRight;

	private Transform tr;
	private SpriteRenderer sr;
	private bool dejaJoue;
	private int choix;	// 0 : right, 1 : up, 2 : left, 3 : down
	private int nbPlayerControl; // en pratique, toujours égal à 4 pour contrôler le monstre mais besoin de respecter l'interface

	private float vitesse;
	private int direction; // comme choix, mais utilisee pour le deplacement (sync avec l'horloge)
	private float nextx;
	private float nexty;
	private float oldx;
	private float oldy;
	private bool collision;
	private float tpsRAZ;


	public int nbPlayerControllers {
		get {
			return nbPlayerControl;
		}
		set {
			nbPlayerControl = value;
		}
	}

	private void respawn () {
		Vector3 respawn0 = Vector3.zero;
		Vector3 respawn1 = new Vector3 (-4.8f, -2.4f, 0f);
		Vector3 respawn2 = new Vector3 (-4.8f, 3.6f, 0f);
		Vector3 respawn3 = new Vector3 (4.8f, 3.6f, 0f);
		Vector3 respawn4 = new Vector3 (4.8f, -3.6f, 0f);
		bool spawnOK = false;

		while (!spawnOK) {
			int n = Random.Range (0, 5);
			switch (n) {
			case 0:
				if ((tr.position - respawn0).sqrMagnitude > taille_case) {
					tr.position = respawn0;
					spawnOK = true;
				}
				break;
			case 1:
				if ((tr.position - respawn1).sqrMagnitude > taille_case) {
					tr.position = respawn1;
					spawnOK = true;
				}
				break;
			case 2:
				if ((tr.position - respawn2).sqrMagnitude > taille_case) {
					tr.position = respawn2;
					spawnOK = true;
				}
				break;
			case 3:
				if ((tr.position - respawn3).sqrMagnitude > taille_case) {
					tr.position = respawn3;
					spawnOK = true;
				}
				break;
			case 4:
				if ((tr.position - respawn4).sqrMagnitude > taille_case) {
					tr.position = respawn4;
					spawnOK = true;
				}
				break;
			}
		}
		oldx = tr.position.x;
		oldy = tr.position.y;
		nextx = tr.position.x;
		nexty = tr.position.y;
	}

	public void moveLeft() {
		if (!dejaJoue) {
			dejaJoue = true;
			choix = 2;
		}
	}

	public void moveRight() {
		if (!dejaJoue) {
			dejaJoue = true;
			choix = 0;
		}
	}

	public void moveUp() {
		if (!dejaJoue) {
			dejaJoue = true;
			choix = 1;
		}
	}

	public void moveDown() {
		if (!dejaJoue) {
			dejaJoue = true;
			choix = 3;
		};
	}

	public void move() {
		switch (choix) {
		case 0: // right
			nextx = tr.position.x + taille_case;
			sr.sprite = sprRight;
			break;
		case 1: // up
			nexty = tr.position.y + taille_case;
			sr.sprite = sprUp;
			break;
		case 2: // left
			nextx = tr.position.x - taille_case;
			sr.sprite = sprLeft;
			break;
		case 3: // down
			nexty = tr.position.y - taille_case;
			sr.sprite = sprDown;
			break;
		}
		direction = choix;
	}

	public void RAZdejaJoue() {
		if (!collision) {
			oldx = nextx;
			oldy = nexty;
		}

		move ();

		dejaJoue = false;
		collision = false;
		choix = -1;
	}


	void Start () {
		tr = this.GetComponent<Transform> ();
		sr = this.GetComponent<SpriteRenderer> ();
		choix = -1;
		dejaJoue = false;
		tpsRAZ = 120;

		vitesse = taille_case / 20;
		nextx = tr.position.x;
		nexty = tr.position.y;
		oldx = nextx;
		oldy = nexty;
		collision = false;
	}

	void Update () {
		if ((nextx != tr.position.x || nexty != tr.position.y) && !collision) {
			switch (direction) {
			case 0: // on va a droite
				if (tr.position.x + vitesse > nextx) {
					tr.position = new Vector3 (nextx, nexty, 0);
				} else {
					tr.Translate (new Vector3 (vitesse, 0, 0));
				}
				break;
			case 1: // on va en haut
				if (tr.position.y + vitesse > nexty) {
					tr.position = new Vector3 (nextx, nexty, 0);
				} else {
					tr.Translate (new Vector3 (0, vitesse, 0));
				}
				break;
			case 2: // on va a gauche
				if (tr.position.x - vitesse < nextx) {
					tr.position = new Vector3 (nextx, nexty, 0);
				} else {
					tr.Translate (new Vector3 (-vitesse, 0, 0));
				}
				break;
			case 3: // on va en bas
				if (tr.position.y - vitesse < nexty) {
					tr.position = new Vector3 (nextx, nexty, 0);
				} else {
					tr.Translate (new Vector3 (0, -vitesse, 0));
				}
				break;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (string.Equals(other.gameObject.tag, "mur")) {
			tr.position = new Vector3(oldx, oldy, 0);
			nextx = oldx;
			nexty = oldy;
			choix = -1;
			collision = true;
		}
		if (string.Equals (other.gameObject.tag, "teleport")) {
			direction = -1;
			choix = -1;
			nextx = tr.position.x;
			nexty = tr.position.y;
			oldx = nextx;
			oldy = nexty;
		}
		if (string.Equals (other.gameObject.tag, "jpublic")) {
			GameObject explosion = Instantiate (particule);
			explosion.GetComponent<Transform> ().position = tr.position;
			respawn ();
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
