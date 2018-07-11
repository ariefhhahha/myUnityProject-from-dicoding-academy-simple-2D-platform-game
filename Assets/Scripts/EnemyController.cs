using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public bool isFacingRight = true; //buat tau si musuh ini lagi hadap mana
	public Transform batas1; //buat batasin musuh biar gak terus ke kiri
	public Transform batas2; //buat batasin musuh biar gak terus ke kanan

	float speed = 3; //kecepatan gerak musuh

	public AudioSource audioKenaTembak;

	Rigidbody2D rigid;
	Animator anim;
	public int HP = 1;
	bool isDie = false;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (isFacingRight && !isDie) {
			KeKanan ();
		} else {
			KeKiri ();
		}

		if (transform.position.x >= batas2.position.x && isFacingRight) {
			Flip ();
		} else if(transform.position.x <= batas1.position.x && !isFacingRight){
			Flip ();
		}
	}

	void TakeDamage(int damage){
		HP -= damage;
		if (HP <= 0) {
			isDie = true;
			GameController.GetInstance().TambahSkor();
			rigid.velocity = Vector2.zero;
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			anim.SetBool ("IsDown", true);
			Destroy (this.gameObject, 1);
		}
	}

	//buat ke kanan
	void KeKanan (){
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;
		if (!isFacingRight) {
			Flip ();
		}
	}

	//buat ke kiri
	void KeKiri(){
		Vector3 pos = transform.position;
		pos.x -= speed * Time.deltaTime;
		transform.position = pos;
		if (isFacingRight) {
			Flip();
		}
	}

	//buat balik badan
	void Flip(){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		isFacingRight = !isFacingRight;
	}
	
	//pas musuh kena tembak
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Peluru")) {
			audioKenaTembak.Play ();
		}
	}
}
