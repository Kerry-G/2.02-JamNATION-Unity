using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu_Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	Coroutine hoverStart;
	Coroutine hoverCredits;
	Coroutine hoverExit;
	Coroutine hoverCredOff;

	private float boutonTemps = 1.5f;
	float cursorTimer;

	public GameObject creditsScreen;

	public GameObject cursor;
	Coroutine cursorScale;

	private Vector3 maxSize;

	void Start(){
		maxSize = new Vector3(1, 1, 1);
	}

	public void OnPointerEnter(PointerEventData eventData){
		if (gameObject.transform.tag == "Button") {
			cursorScale = StartCoroutine (ScaleCursor ());
		}
		if (gameObject.transform.name == "Hover_Start") {
			hoverStart = StartCoroutine (HoverStart ());
		}
		else if (gameObject.transform.name == "Hover_Credits") {
			hoverCredits = StartCoroutine (HoverCredits ());
		}
		else if (gameObject.transform.name == "Hover_Exit") {
			hoverExit = StartCoroutine (HoverExit ());
		}
		else if (gameObject.transform.name == "Back") {
			hoverExit = StartCoroutine (HoverCredOff ());
		}
	}


	public void OnPointerExit(PointerEventData eventData){
		if (gameObject.transform.tag == "Button") {
			StopCoroutine (cursorScale);
			cursor.transform.localScale = new Vector3 (0, 0, 0);
		}
		if (gameObject.transform.name == "Hover_Start") {
			StopCoroutine (hoverStart);
		}
		else if (gameObject.transform.name == "Hover_Credits") {
			StopCoroutine (hoverCredits);
		}
		else if (gameObject.transform.name == "Hover_Exit") {
			StopCoroutine (hoverExit);
		}
		else if (gameObject.transform.name == "Back") {
			StopCoroutine (hoverCredOff);
		}

	}

	IEnumerator ScaleCursor() {
		cursorTimer = 0;
		while (true) {
			cursorTimer += Time.deltaTime;
			cursor.transform.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, cursorTimer / boutonTemps);
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator HoverStart() {
		yield return new WaitForSeconds (boutonTemps);
		StartGame ();
		yield return null;
	}
	IEnumerator HoverCredits() {
		yield return new WaitForSeconds (boutonTemps);
		Credits ();
		yield return null;
	}
	IEnumerator HoverExit() {
		yield return new WaitForSeconds (boutonTemps);
		Quit ();
		yield return null;
	}
	IEnumerator HoverCredOff() {
		yield return new WaitForSeconds (boutonTemps);
		CredOff ();
		yield return null;
	}

	public void StartGame () {
		Debug.Log ("Start");
//		SceneManager.LoadScene (1);
	}

	public void Credits () {
		Debug.Log ("Credits");
		creditsScreen.GetComponent<Animator> ().SetBool ("CreditsOn", true);
	}

	public void Quit () {
		Debug.Log ("Quit");
	//	Application.Quit ();
	}

	public void CredOff () {
		Debug.Log ("CredOff");
		creditsScreen.GetComponent<Animator> ().SetBool ("CreditsOn", false);
	}

		
}


