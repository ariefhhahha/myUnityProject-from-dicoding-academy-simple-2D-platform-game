using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnalogController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image analogContainer;
	private Image analog;
	public Vector3 inputDirection;

	// Use this for initialization
	void Start () {
		analogContainer = GetComponent<Image> ();
		analog = transform.GetChild (0).GetComponent<Image> ();
		inputDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(PointerEventData ped){
		Vector2 pos = Vector2.zero;

		//mendapatkan arah input
		RectTransformUtility.ScreenPointToLocalPointInRectangle (analogContainer.rectTransform, ped.position, ped.pressEventCamera, out pos);

		pos.x = (pos.x / analogContainer.rectTransform.sizeDelta.x);
		pos.y = (pos.y / analogContainer.rectTransform.sizeDelta.y);

		float x = (analogContainer.rectTransform.pivot.x == 1f) ? pos.x * 2 + 1 : pos.x * 2 - 1;
		float y = (analogContainer.rectTransform.pivot.y == 1f) ? pos.y * 2 + 1 : pos.y * 2 - 1;

		inputDirection = new Vector3 (x, y, 0);
		inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

		//untuk menentukan area tempat analog bergerak
		analog.rectTransform.anchoredPosition = new Vector3 (inputDirection.x * (analogContainer.rectTransform.sizeDelta.x / 3), inputDirection.y * (analogContainer.rectTransform.sizeDelta.y) / 3);

	}

	public void OnPointerDown(PointerEventData ped){
		OnDrag (ped);
	}

	public void OnPointerUp(PointerEventData ped){
		inputDirection = Vector3.zero;
		analog.rectTransform.anchoredPosition = Vector3.zero;
	}
}
