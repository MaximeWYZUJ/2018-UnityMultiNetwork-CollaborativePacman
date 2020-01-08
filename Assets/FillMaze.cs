using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillMaze : MonoBehaviour {

	private Transform tr;
	private float taille_case;

	void Start() {
		taille_case = 1.2f;
		tr = GetComponent<Transform> ();
		int indiceX = Mathf.RoundToInt(tr.position.x / taille_case);
		int indiceY = Mathf.RoundToInt(tr.position.y / taille_case);
		GameObject.Find ("LevelManager").GetComponent<ConstructorMaze> ().maze [indiceX + 5, indiceY + 5] = 1;
	}
}
