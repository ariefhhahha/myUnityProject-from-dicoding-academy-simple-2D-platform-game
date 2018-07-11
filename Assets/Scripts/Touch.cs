using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Touch : MonoBehaviour {

	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}

	public void LeftArrow() {
		player.moveRight = false;
		player.moveLeft = true;
	}

	public void RightArrow() {
		player.moveRight = true;
		player.moveLeft = false;
	}

	public void ReleaseLeftArrow() {
		player.moveLeft = false;
	}

	public void ReleaseRightArrow(){
		player.moveRight = false;
	}

	public void Jump(){
		player.jump = true;
	}

	public void NoJump(){
		player.jump = false;
	}

	public void Shoot(){
		player.tembak = true;
	}

	public void NoShoot(){
		player.tembak = false;
	}

	public void Exit(){
		Debug.Log ("Quit");
		SceneManager.LoadScene (2);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
