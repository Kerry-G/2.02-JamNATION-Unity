using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
//		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Input.mousePosition;
	}
}
