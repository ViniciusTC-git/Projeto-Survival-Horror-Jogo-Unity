using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollsion : MonoBehaviour {
	public Transform target;
	RaycastHit hit;
	public float distanciaCamera;
	public float ajusteCamera;
	void Start(){
		
	}
	void Update(){
		Vector3 rotacao = transform.eulerAngles;
		rotacao.z = 0;
		transform.eulerAngles = rotacao;
		transform.position = target.position - transform.forward * distanciaCamera;
		if (Physics.Linecast (target.position, transform.position, out hit)) {
			transform.position = hit.point + transform.forward * ajusteCamera;
		}
	}

	
}
