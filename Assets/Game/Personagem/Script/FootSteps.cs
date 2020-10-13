using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FootSteps : MonoBehaviour
{
	[SerializeField]
    public AudioClip[] som;
    private AudioSource audioSource;
	private CharacterController controller;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
		controller = GetComponent<CharacterController> ();
    }
	void Step(){
		if (controller.velocity.magnitude != 0) {
			if (controller.velocity.magnitude > 8) {
			   audioSource.clip = som[2];
			} else {
			   audioSource.clip = som [Random.Range (0, som.Length-1)];
			}
			audioSource.Play ();
		}
		
	}
		 
}