using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escada : MonoBehaviour {

	public bool blockKey = false; 

	void OnTriggerStay(Collider player){
		if (player.gameObject.tag == "Player" && gameObject.tag == "escada") {
			player.GetComponent<ControlePersonagem> ().lockRun = true;
			//Debug.Log ("ESTA NA ESCADA");
		}
	}
	void OnTriggerExit(Collider player){
		player.GetComponent<ControlePersonagem> ().lockRun = false;
	}

}
