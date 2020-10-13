using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaPosicoes : CenarioController {
	public Transform player;
	[SerializeField]
	public AudioClip som;
	private AudioSource audioSource;

	private void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

	public override void Start () {
		base.Start();
		switch (prevScene) {
		case "CenaCorredorAndar2":
			player.position = new Vector3 (-21.59f, 29.72f, 16.38f);
			break;
		}
		audioSource.clip = som;
		audioSource.Play ();
	}
	

}
