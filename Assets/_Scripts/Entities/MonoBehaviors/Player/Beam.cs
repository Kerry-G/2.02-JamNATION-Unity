using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.PlayerLoop;

[RequireComponent (typeof(LineRenderer))]

public class Beam : MonoBehaviour {

	public float laserWidth = 1.0f;
	public float laserLenght = 100f;
	private LineRenderer _lineRenderer;

	void Start() {
		GameController.Instance.onChangePhase.AddListener(ChangePhaseListener);

		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.positionCount = 2; //number of vertices
		_lineRenderer.startWidth = laserWidth;
		_lineRenderer.endWidth = laserWidth;
		_lineRenderer.SetPosition(0, new Vector3(-10,-10,-10));
		_lineRenderer.SetPosition(1, new Vector3(-10, -10, -10));
	}


	private void ChangePhaseListener(GamePhase changedTo) {
		if(changedTo == GamePhase.Moving)
			Shoot();
	}


	private void Shoot() {
		Debug.Log("Player \""+gameObject.name+"\" is shooting.");

		RaycastHit hit;
		Vector3    start = transform.position + Vector3.up;

		if ( Physics.Raycast(start, transform.forward, out hit, laserLenght) ) {
			if ( hit.collider.gameObject.transform.parent.name.Contains("Player")
			     && hit.collider.gameObject.transform.parent.name != gameObject.name ) {
				if ( GameController.Instance.IsTesting() )
					Debug.Log(gameObject.name + "Hit player: " + hit.collider.gameObject.transform.parent.name);

				hit.collider.gameObject.transform.parent.gameObject.BroadcastMessage("Kill");
			}

			_lineRenderer.SetPosition(0, start);
			_lineRenderer.SetPosition(1, hit.point);

			Debug.Log("in");
		} else {
			_lineRenderer.SetPosition(0, start);
			_lineRenderer.SetPosition(1, transform.forward * laserLenght);
			Debug.Log("Not in");
		}
	}

}
