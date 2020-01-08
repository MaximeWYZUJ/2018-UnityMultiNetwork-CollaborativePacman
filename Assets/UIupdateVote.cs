using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIupdateVote : MonoBehaviour {

	public GameObject arrowUp;
	public GameObject arrowLeft;
	public GameObject arrowDown;
	public GameObject arrowRight;
	public GameObject pacman;

	private Text txtUp;
	private Text txtLeft;
	private Text txtDown;
	private Text txtRight;



	void Start () {
		txtUp = arrowUp.GetComponent<Text> ();
		txtLeft = arrowLeft.GetComponent<Text> ();
		txtDown = arrowDown.GetComponent<Text> ();
		txtRight = arrowRight.GetComponent<Text> ();
	}
	

	void Update () {
		txtUp.text = pacman.GetComponent<MovePublic> ().voteHaut.ToString();
		txtLeft.text = pacman.GetComponent<MovePublic> ().voteGauche.ToString();
		txtDown.text = pacman.GetComponent<MovePublic> ().voteBas.ToString();
		txtRight.text = pacman.GetComponent<MovePublic> ().voteDroit.ToString();
	}
}
