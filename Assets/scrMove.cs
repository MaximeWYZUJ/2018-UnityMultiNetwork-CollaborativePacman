using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface scrMove {
	int nbPlayerControllers{ get; set;}

	void RAZdejaJoue();
	void moveLeft();
	void moveRight();
	void moveUp();
	void moveDown();
	void move();
}
