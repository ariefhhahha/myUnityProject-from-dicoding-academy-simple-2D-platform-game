using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

	public string enterScene;
	public string escapeScene;
	public bool isEscapeForQuit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Return)) {
			SceneManager.LoadScene (enterScene);
		}

		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (isEscapeForQuit) {
				Application.Quit ();
				Debug.Log ("Quit");
			} else {
				SceneManager.LoadScene (escapeScene);
			}
		}
	}
}
