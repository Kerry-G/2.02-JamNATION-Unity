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
			setWinner("shark");
		} else if (index == 3) {
			setWinner("lion");

		} else if (index == 1) {
			setWinner("cow");

		} else if (index == 0) {
			setWinner("bear");
		}
		//round draw
		else if (index == -1) {
			setWinner("draw");
		}
		//Final winner
		else if (index == 12) {
			setBigWinner("shark");
		} else if (index == 13) {
			setBigWinner("lion");
		} else if (index == 11) {
			setBigWinner("cow");
		} else if (index == 10) {
			setBigWinner("bear");
		}
				
		for (int i = 0; i < sharkWins; i++) {
			shark.transform.GetChild (i).gameObject.SetActive (true);
		}
		for (int i = 0; i < lionWins; i++) {
			lion.transform.GetChild (i).gameObject.SetActive (true);
		}
		for (int i = 0; i < cowWins; i++) {
			cow.transform.GetChild (i).gameObject.SetActive (true);
		}
		for (int i = 0; i < bearWins; i++) {
			bear.transform.GetChild (i).gameObject.SetActive (true);
		}

	}


	private void setWinner(string a) {
		shark.transform.Find("SharkWin").gameObject.SetActive(false);
		lion.transform.Find("LionWin").gameObject.SetActive(false);
		cow.transform.Find("CowWin").gameObject.SetActive(false);
		bear.transform.Find("BearWin").gameObject.SetActive(false);
		draw.SetActive (false);
		if (a == "shark") shark.transform.Find("SharkWin").gameObject.SetActive(true);
		if (a == "lion") lion.transform.Find("LionWin").gameObject.SetActive(true);
		if (a == "cow") cow.transform.Find("CowWin").gameObject.SetActive(true);
		if (a == "bear") bear.transform.Find("BearWin").gameObject.SetActive(true);
		if (a == "draw") draw.SetActive (true);
	}
	private void setBigWinner(string a) {
		shark.transform.Find("SharkWin").gameObject.SetActive(false);
		lion.transform.Find("LionWin").gameObject.SetActive(false);
		cow.transform.Find("CowWin").gameObject.SetActive(false);
		bear.transform.Find("BearWin").gameObject.SetActive(false);
		draw.SetActive (false);
		if (a == "shark") sharkWin.SetActive (true);
		if (a == "lion") lionWin.SetActive (true);
		if (a == "cow") cowWin.SetActive (true);
		if (a == "bear") bearWin.SetActive (true);

	
	}
	
}
