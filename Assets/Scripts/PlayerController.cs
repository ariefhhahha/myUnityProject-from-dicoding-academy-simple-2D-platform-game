using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Animator anim; //bikin variable anim yang tipenya animator (game component itu tuh)
	Rigidbody2D rigid; //bikin variable rigid yang tipenya rigidbody2d (game component)

	public bool isGrounded = false; //buat nyimpen state kalo karakter lagi nginjek tanah (ground)
	public bool isFacingRight = true; //buat tau karakter ngehadap kemana
	public float jumpForce = 200f; //gaya keatas dari karakter
	public float walkForce = 15f; //gaya dorong kesamping dari karakter
	public float maxSpeed = 4f; //kecepatan maksimal karakter
	public bool moveRight; //biar ke kanan
	public bool moveLeft; //biar ke kiri
	public bool jump; //biar ke atas
	public bool tembak; //biar nembak

	public GameObject peluru; //bikin gameobject peluru
	public Vector2 kecepatanPeluru = new Vector2(50, 0); //buat kecepatan peluru melesat
	public Vector2 peluruOffset = new Vector2 (0.5f, 0.049f); //jarak peluru dari karakter
	public float jedaPeluru = 1f; //jeda waktu menembak
	public bool bisaTembak = true; //memastikan player bisa nembak

	bool hidup = true; //mastiin si karakter masih hidup atau tydaq
	//kalo karakter masih hidup, dia bisa bergerak dan menembak
	//kalo karakter mati, tidak bisa bergerak dan menembak

	public AudioSource audioLompat; //suara lompat
	public AudioSource audioTembak; //suara tembakan
	public AudioSource audioPlayerMati; //suara player mati karena kena zombie

	/*public AnalogController analogMovement;
	private Vector3 direction;
	private float xMin, xMax, yMin, yMax;*/

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> (); //ngisi variable anim tadi sama component animator
		rigid = GetComponent<Rigidbody2D> (); //ngisi variable rigid tadi sama component rigidbody2d

		/*xMax = Screen.width - 5;
		xMin = 5;
		yMax = Screen.height - 5;
		yMin = 5;*/
	}
	
	// Update is called once per frame
	void Update () {
		InputHandler (); //jalankan method InputHandler
		anim.SetInteger ("Speed", (int)rigid.velocity.x); //set parameter di animator tadi, veloxity.x masih float jadi harus ubah dulu ke integer
		if (transform.position.y < -7) { //---------------
			SceneManager.LoadScene (2);
		}

		if (gameObject.GetComponent<Transform> ().position.x >= 68) { //kalo karakter udah nyampe ujung map, permainan berakhir
			SceneManager.LoadScene (2);
		}

		/*direction = analogMovement.inputDirection;

		if (direction.magnitude != 0) {
			transform.position += direction * 1;
			//transform.position = new Vector3 (Mathf.Clamp (transform.position.x, xMin, xMax), Mathf.Clamp (transform.position.y, yMin, yMax), 0f);
			rigid.AddForce(new Vector2(Mathf.Clamp (transform.position.x, xMin, xMax), Mathf.Clamp (transform.position.y, yMin, yMax)));
		}*/
	}

	//buat ngirim status hidup ke gamecontroller
	void StatusHidup(){
	} 

	//buat nanganin input player
	void InputHandler() {
		if ((Input.GetKey (KeyCode.LeftArrow) || moveLeft) && hidup) { //kalo diteken tombol panah kiri
			KeKiri ();
		}
		if ((Input.GetKey (KeyCode.RightArrow) || moveRight) && hidup) { //kalo diteken tombol panah kanan
			KeKanan ();
		}
		if ((Input.GetKeyDown (KeyCode.UpArrow) || jump) && hidup) { //kalo diteken tombol panah atas
			if (jump) {
				LompatTouch ();
			} else {
				Lompat ();
			}
		}

		if ((Input.GetKey (KeyCode.Space) || tembak) && hidup) { //kalo diteken spasi
			Tembak ();
		}
	}

	//buat ke kiri
	void KeKiri(){
		if (rigid.velocity.x * -1 < maxSpeed) {
			rigid.AddForce (Vector2.left * walkForce);
		}

		//balik arah karakter ke arah yang berlawanan dari seharusnya
		if (isFacingRight) {
			Balik ();
		}
	}

	//buat ke kanan
	void KeKanan(){
		if (rigid.velocity.x * 1 < maxSpeed) {
			rigid.AddForce (Vector2.right * walkForce);
		}

		//balik arah karakter ke arah yang berlawanan dari seharusnya
		if (!isFacingRight) {
			Balik ();
		}
	}

	//buat lompat
	void Lompat(){
		if (isGrounded == true) {
			rigid.AddForce (Vector2.up * jumpForce);
			audioLompat.Play (); //biar bersuara
		}
	}

	void LompatTouch(){
		if (isGrounded == true) {
			rigid.AddForce (Vector2.up * 250);
			audioLompat.Play (); //biar bersuara
		}
	}

	//buat nembak
	void Tembak() {
		if(bisaTembak){
			anim.SetTrigger ("Shoot");

			//buat peluru baru
			GameObject proyektil = (GameObject)Instantiate (peluru, (Vector2)transform.position + peluruOffset * transform.localScale.x, Quaternion.identity);

			audioTembak.Play (); //biar bersuara

			//atur kecepatan peluru
			Vector2 kecepatan = new Vector2(kecepatanPeluru.x * transform.localScale.x, kecepatanPeluru.y);
			proyektil.GetComponent<Rigidbody2D> ().velocity = kecepatan;

			//menyesuaikan scale dari peluru dengan scale karakter
			Vector3 scale = transform.localScale;
			proyektil.transform.localScale = scale;
			StartCoroutine (CanShoot ());
		}
	}

	//bikin jeda tembakan
	IEnumerator CanShoot() {
		bisaTembak = false;
		yield return new WaitForSeconds (jedaPeluru); //tunggu tiap detik
		bisaTembak = true;
	}

	//buat balik badan
	void Balik(){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		isFacingRight = !isFacingRight;
	}
		
	//pas karakter mulai nginjek tanah
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("Ground")){ //kalo kena gameobjek yang tag-nya "Ground"
			anim.SetBool("IsGrounded", true); //set parameter yang ada di animator tadi
			isGrounded = true; //set variable 
		}
		if (col.gameObject.CompareTag ("Enemy")) { //kalo kena gameobjek yang tag-nya "enemy"
			audioPlayerMati.Play();
			rigid.velocity = transform.up * 5; //jadi kelempar ke atas dulu
			hidup = false;
			gameObject.GetComponent<BoxCollider2D> ().enabled = false; //baru lepas boxcollidenyar, nanti bakal jatoh kebawa
		}
	}
		
	//pas karakter masih nginjek tanah 
	void OnCollisionStay2D(Collision2D col) { 
		if (col.gameObject.CompareTag("Ground")){ //kalo kena gameobjek yang tag-nya "Ground"
			anim.SetBool("IsGrounded", true); //set parameter yang ada di animator tadi
			isGrounded = true; //set variable 
		}
	}

	//pas karakter udah gak nginjek tanah
	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.CompareTag("Ground")){ //kalo udh lepas dari gameobjek yang tag-nya "Ground"
			anim.SetBool ("IsGrounded", false); //set paramita eh parameter yang ada di animator tadi
			isGrounded = false; //set variable
		}
	}
}
