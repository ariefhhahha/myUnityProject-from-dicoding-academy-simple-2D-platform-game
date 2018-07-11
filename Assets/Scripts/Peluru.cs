using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peluru : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Hancurkan ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator Hancurkan() {
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
	}
		

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Player")) {
			return;
		}
		if (col.gameObject.CompareTag ("Enemy")) {
			col.gameObject.SendMessage ("TakeDamage", 1);
		}
		Destroy (this.gameObject);
	}

}
