using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextWin : MonoBehaviour {


	public string whoWins;
	public int randomValue;

	public int sharkWins;
	public int lionWins;
	public int cowWins;
	public int bearWins;

	public GameObject shark;
	public GameObject lion;
	public GameObject cow;
	public GameObject bear;

	// Use this for initialization
	void Start () {
		StartCoroutine (Timer (1f));
	}
	
	// Update is called once per frame
	void Update () {
		
		if (whoWins == "Shark") {
			gameObject.GetComponent<Text> ().text = "Shark Wins";
			gameObject.GetComponent<Text> ().color = new Color32 (0x02, 0xE8, 0xFF, 0xFF);
		} else if (whoWins == "Lion") {
			gameObject.GetComponent<Text> ().text = "Lion Wins";
			gameObject.GetComponent<Text> ().color = new Color32 (0xE9, 0x6E, 0x14, 0xFF);
		} else if (whoWins == "Cow") {
			gameObject.GetComponent<Text> ().text = "Cow Wins";
			gameObject.GetComponent<Text> ().color = new Color32 (0x81, 0xE9, 0x1D, 0xFF);
		} else if (whoWins == "Bear") {
			gameObject.GetComponent<Text> ().text = "Bear Wins";
			gameObject.GetComponent<Text> ().color = new Color32 (0xA5, 0x25, 0xD3, 0xFF);
		} else if (whoWins == "Draw") {
			gameObject.GetComponent<Text> ().text = "Draw";
			gameObject.GetComponent<Text> ().color = new Color32 (0x32, 0x32, 0x32, 0xFF);
		}

		if (sharkWins > 8 || lionWins > 8 || cowWins > 8 || bearWins > 8) {
			SceneManager.LoadScene (0);
		}
	}

	IEnumerator Timer(float temps) {
		randomValue = Random.Range (1, 6);
		if (randomValue == 1) {
			whoWins = "Shark";
			sharkWins++;
			for (int i = 0; i < sharkWins; i++) {
				shark.transform.GetChild (i).gameObject.SetActive (true);
			}
		} else if (randomValue == 2) {
			whoWins = "Lion";
			lionWins++;
			for (int i = 0; i < lionWins; i++) {
				lion.transform.GetChild (i).gameObject.SetActive (true);
			}
		} else if (randomValue == 3) {
			whoWins = "Cow";
			cowWins++;
			for (int i = 0; i < cowWins; i++) {
				cow.transform.GetChild (i).gameObject.SetActive (true);
			}
		} else if (randomValue == 4) {
			whoWins = "Bear";
			bearWins++;
			for (int i = 0; i < bearWins; i++) {
				bear.transform.GetChild (i).gameObject.SetActive (true);
			}
		} else if (randomValue == 5) {
			whoWins = "Draw";
		}
		yield return new WaitForSeconds (temps);
		StartCoroutine (Timer (1f));
		yield return null;
	}
}
