using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.PlayerLoop;

[RequireComponent (typeof(LineRenderer))]

public class Beam : MonoBehaviour {

	public float laserWidth = 1.0f;
	public float laserLenght = 100f;
	private float inFadeLaser = 0.5f;
	public float laserFadeTime = .002f;
	private LineRenderer _lineRenderer;

	private float _timerOut;
	private float _timerIn;
	
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
		RaycastHit hit;
		Vector3    start = transform.position + Vector3.up;
		
		
		if ( Physics.Raycast(start, transform.forward, out hit, laserLenght) ) {
			if ( hit.collider.gameObject.transform.parent.name.Contains("Player")
			     && hit.collider.gameObject.transform.parent.name != gameObject.name ) {
				hit.collider.gameObject.transform.parent.gameObject.BroadcastMessage("Kill");
			}
			_lineRenderer.SetPosition(0, start);
//			_lineRenderer.SetPosition(1, hit.point);
			StartCoroutine(BeamLaunch(start, hit.point));
			
		} else {
			_lineRenderer.SetPosition(0, start);
			StartCoroutine(BeamLaunch(start, transform.forward * laserLenght));
		}

	}

	IEnumerator BeamLaunch(Vector3 start, Vector3 hit) {
		while ( true ) {
			_timerIn += Time.deltaTime;
			Vector3 h = Vector3.Lerp(start, hit, _timerIn*10);
			_lineRenderer.SetPosition(1, h);
			if ( _timerIn > inFadeLaser ) {
				StartCoroutine(FadeBeam(start, hit));
				_timerIn = 0f;
				yield break;
			}
			yield return new WaitForEndOfFrame();
		}
	}
	
	IEnumerator FadeBeam(Vector3 start, Vector3 hit) {
		while ( true ) {
			
			_timerOut += Time.deltaTime;
			Vector3 h = Vector3.Lerp(start, hit, _timerOut * 8);
			_lineRenderer.SetPosition(0, h);
			
			if ( _timerOut > laserFadeTime ) {
				_lineRenderer.SetPosition(0, Vector3.zero);
				_lineRenderer.SetPosition(1, Vector3.zero);
				_timerOut = 0f;
				yield break;
			}
			yield return new WaitForEndOfFrame();
		}
	}
	

}
