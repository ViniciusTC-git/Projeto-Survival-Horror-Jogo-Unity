using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnterDoor : MonoBehaviour {

	private Animator anim;//ANIMACAO
	private Transform playerPosition;//POSICAO DO PERSONAGEM
	private ControlePersonagem controlScript;//SCRIPT CONTROLE DO PERSONAGEM
//	private CharacterController playerControl;
	private CenarioController sceneController;//CONTROLE DE CENAS

	public AudioClip[] som;//SOM
	public AudioSource audioSource;//GATILHO DO SOM
	public GameObject player;//PERSONAGEM
	public GameObject loading;//CARREGAMENTO TELA
	public Transform doorPosition;//POSICAO PARA ANIMAÇÃO
	public Transform verificaFrente;
	public string tipoPorta;//TIPO DE PORTAA
	[SerializeField] private string cena = "";//PROXIMA CENA


    void Start()
    {
		loading.SetActive(false);
		audioSource = GetComponent<AudioSource>();
		anim = player.GetComponent<Animator>();
		playerPosition = player.GetComponent<Transform> ();
		controlScript = player.GetComponent<ControlePersonagem> ();
		//playerControl = player.GetComponent<CharacterController> ();
		sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<CenarioController>();
    }
    void OnTriggerStay(Collider player)
    {
		if(doorPosition.gameObject.name == "PositionRight"){ 
			anim.SetBool ("Mirror", true); 
		}else{
			anim.SetBool ("Mirror", false);
		}

		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("OpenDoor")) {
			controlScript.lockControl = false;
		} else {
			controlScript.lockControl = true;
		}

		if (Input.GetKeyDown (KeyCode.E) && checkForward()) {
			if (player.gameObject.tag == "Player" && tipoPorta != "Fechada") {
				loading.SetActive (true);	
				audioSource.PlayOneShot (som[1]);
				StartCoroutine (Delay ("cenario",1f));
			} else {
				if (!audioSource.isPlaying && !anim.GetCurrentAnimatorStateInfo (0).IsName("OpenDoor")){
					playerPosition.eulerAngles = new Vector3(playerPosition.eulerAngles.x, doorPosition.eulerAngles.y, playerPosition.eulerAngles.z);
					playerPosition.position = doorPosition.position;
					anim.Play("OpenDoor", -1, 0f);
					StartCoroutine(Delay("somPortaTrancada",1f));
				}

			}
		}

    }

	//VERIFICA SE O PERSONAGEM ESTA A FRENTE DA PORTA
	bool checkForward(){
		
		float angulo =  Vector3.Angle (playerPosition.forward,verificaFrente.position-transform.position);
		if (Mathf.Abs(angulo) >= 70 && Mathf.Abs(angulo) <=80) {
			//Debug.Log(gameObject.transform.parent.name+" ESTA A FRENTE ANGULO:"+angulo);
			return true;
		}
		//Debug.Log(gameObject.transform.parent.name+" NÃO ESTA A FRENTE ANGULO:"+angulo);
		return false;
	}
		

	IEnumerator Delay(string action,float time){
		yield return new WaitForSeconds(time);
		switch(action){
			case "cenario":
				sceneController.LoadScene (cena);
				break;	
			case "somPortaTrancada":
				audioSource.PlayOneShot (som[0]);
				break;

		}
	}
}
