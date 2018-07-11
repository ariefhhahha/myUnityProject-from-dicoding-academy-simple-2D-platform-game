using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int total_skor = 0; //buat catet skor
	public Text labelSkor;//buat menampilkan skor

	private static GameController instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void TambahSkor(){
		total_skor += 10;
		labelSkor.text = "Skor : " + total_skor.ToString ();
	}

	public static GameController GetInstance(){
		return instance;
	}
}
