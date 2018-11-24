using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

[RequireComponent (typeof(LineRenderer))]

public class Beam : MonoBehaviour {

	public float laserWidth = 1.0f;
	public float laserLenght = 100f;
	private LineRenderer _lineRenderer;
	
	void Start() {
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.positionCount = 2; //number of vertices 
		_lineRenderer.startWidth = laserWidth;
		_lineRenderer.endWidth = laserWidth;
		_lineRenderer.SetPosition(0, new Vector3(-10,-10,-10));
		_lineRenderer.SetPosition(1, new Vector3(-10, -10, -10));
	}


	private void FixedUpdate() {
		if ( GameController.Instance.IsTesting() ) Shoot();
	}
	
	private void Shoot() {
		RaycastHit hit;
		Vector3    start = transform.position + Vector3.up;

		if ( Physics.Raycast(start, transform.forward, out hit, 100.0f) ) {
			print("Found an object: " + hit.collider.gameObject + " - distance: " + hit.distance);

			if ( hit.collider.gameObject.transform.parent.name.Contains("Player") ) {
				hit.collider.gameObject.transform.parent.gameObject.BroadcastMessage("Kill");				
			}  
			
			_lineRenderer.SetPosition(0, start);
			_lineRenderer.SetPosition(1, hit.point);
		} else {
			_lineRenderer.SetPosition(0, start);
			_lineRenderer.SetPosition(1, transform.forward * laserLenght);
		}
	}

}
