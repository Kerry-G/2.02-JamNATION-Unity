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
	public GameObject draw;

	public GameObject sharkWin;
	public GameObject lionWin;
	public GameObject cowWin;
	public GameObject bearWin;


	public void setMenu(int index) {

		
		if (index == 2) {
			shark.SetActive (true);
			lion.SetActive (false);
			cow.SetActive (false);
			bear.SetActive (false);
			draw.SetActive (false);
//			for (int i = 0; i <= sharkWins; i++) {
//				shark.transform.GetChild (i).gameObject.SetActive (true);
//			}
		} else if (index == 3) {
			shark.SetActive (false);
			lion.SetActive (true);
			cow.SetActive (false);
			bear.SetActive (false);
			draw.SetActive (false);
//			for (int i = 0; i <= lionWins; i++) {
//				lion.transform.GetChild (i).gameObject.SetActive (true);
//			}
		} else if (index == 1) {
			shark.SetActive (false);
			lion.SetActive (false);
			cow.SetActive (true);
			bear.SetActive (false);
			draw.SetActive (false);
//			for (int i = 0; i <= cowWins; i++) {
//				cow.transform.GetChild (i).gameObject.SetActive (true);
//			}
		} else if (index == 0) {
			shark.SetActive (false);
			lion.SetActive (false);
			cow.SetActive (false);
			bear.SetActive (true);
			draw.SetActive (false);
//			for (int i = 0; i <= bearWins; i++) {
//				bear.transform.GetChild (i).gameObject.SetActive (true);
//			}
		}
		//round draw
		else if (index == -1) {
			shark.SetActive (false);
			lion.SetActive (false);
			cow.SetActive (false);
			bear.SetActive (false);
			draw.SetActive (true);
		}

		//Final winner
		else if (index == 12) {
			shark.SetActive (false);
			lion.SetActive (false);
			cow.SetActive (false);
			bear.SetActive (false);
			draw.SetActive (false);
			sharkWin.SetActive (true);
//			for (int i = 0; i <= sharkWins; i++) {
//				shark.transform.GetChild (i).gameObject.SetActive (true);
//			}
		} else if (index == 13) {
			shark.SetActive (false);
			lion.SetActive (false);
			cow.SetActive (false);
			bear.SetActive (false);
			draw.SetActive (false);
			lionWin.SetActive (true);
//			for (int i = 0; i <= lionWins; i++) {
//				lion.transform.GetChild (i).gameObject.SetActive (true);
//			}
		} else if (index == 11) {
			shark.SetActive (false);
			lion.SetActive (false);
			cow.SetActive (false);
			bear.SetActive (false);
			draw.SetActive (false);
			cowWin.SetActive (true);
//			for (int i = 0; i <= cowWins; i++) {
//				cow.transform.GetChild (i).gameObject.SetActive (true);
//			}
		} else if (index == 10) {
			shark.SetActive (false);
			lion.SetActive (false);
			cow.SetActive (false);
			bear.SetActive (false);
			draw.SetActive (false);
			bearWin.SetActive (true);
//			for (int i = 0; i <= bearWins; i++) {
//				bear.transform.GetChild (i).gameObject.SetActive (true);
//			}
		}
				
		for (int i = 0; i <= sharkWins; i++) {
			shark.transform.GetChild (i).gameObject.SetActive (true);
		}
		for (int i = 0; i <= lionWins; i++) {
			lion.transform.GetChild (i).gameObject.SetActive (true);
		}
		for (int i = 0; i <= cowWins; i++) {
			cow.transform.GetChild (i).gameObject.SetActive (true);
		}
		for (int i = 0; i <= bearWins; i++) {
			bear.transform.GetChild (i).gameObject.SetActive (true);
		}

	}

	
}
