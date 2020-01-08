using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorMaze : MonoBehaviour {

	public int[,] maze;
	public float taille_case;
	public GameObject coin;

	private bool mazeFilled;

	void Awake() {
		maze = new int[11, 11];

		for (int i = 0; i < 11; i++) {
			for (int j = 0; j < 11; j++) {
				maze [i, j] = 0;
			}
		}

		mazeFilled = false;
	}

	void Start() {
		StartCoroutine (UpdateCoroutine());
	}

	IEnumerator UpdateCoroutine () {
		GameObject c0 = null;
		while (!mazeFilled) {
			for (int j = 0; j < 11; j++) {
				for (int i = 0; i < 11; i++) {
					if (maze [i, j] == 0) { // il n'y a rien à cet emplacement (mur, pacman ou monstre)
						if (i == 0 && j == 0) {
							c0 = Instantiate (coin);
							c0.GetComponent<Transform> ().position = new Vector3 (i - 5, j - 5, 0) * taille_case;
							yield return new WaitForSecondsRealtime (0.03f);
						} else {
							GameObject c = Instantiate (coin);
							c.GetComponent<Transform> ().position = new Vector3 (i - 5, j - 5, 0) * taille_case;
							yield return new WaitForSecondsRealtime (0.03f);
						}
					}
				}
			}

			if (c0 != null) {
				Destroy (c0);
			}


			mazeFilled = true;

			yield return null;
		}
	}
}
