using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextWin : MonoBehaviour {

	public int sharkWins;
	public int lionWins;
	public int cowWins;
	public int bearWins;

	public GameObject shark;
	public GameObject lion;
	public GameObject cow;
	public GameObject bear;



	public void setMenu(int index) {
				
		if (index == 2) {
			gameObject.GetComponent<Text> ().text  = "Shark Wins";
			gameObject.GetComponent<Text> ().color = new Color32 (0x02, 0xE8, 0xFF, 0xFF);
			for (int i = 0; i <= sharkWins; i++) {
				shark.transform.GetChild (i).gameObject.SetActive (true);
			}
		} else if (index == 3) {
			gameObject.GetComponent<Text> ().text  = "Lion Wins";
			gameObject.GetComponent<Text> ().color = new Color32 (0xE9, 0x6E, 0x14, 0xFF);
			for (int i = 0; i <= lionWins; i++) {
				lion.transform.GetChild (i).gameObject.SetActive (true);
			}
		} else if (index == 1) {
			gameObject.GetComponent<Text> ().text  = "Cow Wins";
			gameObject.GetComponent<Text> ().color = new Color32 (0x81, 0xE9, 0x1D, 0xFF);
			for (int i = 0; i <= cowWins; i++) {
				cow.transform.GetChild (i).gameObject.SetActive (true);
			}
		} else if (index == 0) {
			gameObject.GetComponent<Text> ().text  = "Bear Wins";
			gameObject.GetComponent<Text> ().color = new Color32 (0xA5, 0x25, 0xD3, 0xFF);
			for (int i = 0; i <= bearWins; i++) {
				bear.transform.GetChild (i).gameObject.SetActive (true);
			}
		} else if (index == -1) {
			gameObject.GetComponent<Text> ().text  = "Draw";
			gameObject.GetComponent<Text> ().color = new Color32 (0x32, 0x32, 0x32, 0xFF);
		}
	}

	
}
