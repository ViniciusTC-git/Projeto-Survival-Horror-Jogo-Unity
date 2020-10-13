using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPick : MonoBehaviour {

	private CenarioController sceneController;//CONTROLE DE CENAS
	private Animator anim;//ANIMACAO
	private ControlePersonagem controlScript;//SCRIPT CONTROLE DO PERSONAGEM
	private Inventario inventario;
	private MenuController menuItem;

	public GameObject item;
	public GameObject player;//PERSONAGEM
	public string tipoItem;//TIPO DO ITEM
	public string nomeItem;
	public AudioSource audioSource;//GATILHO DO SOM
	public AudioClip som;//SOM
	//private string maoDireita = "Armature/COG/Body_0/backbone1/backbone2/chest/shoulder_R/arm_R/forearm1_R/forearm2_R/palm_R/mid1_R/Item_R";
	void Start () {
		anim = player.GetComponent<Animator>();
		controlScript = player.GetComponent<ControlePersonagem> ();
		audioSource = GetComponent<AudioSource>();
		inventario = player.GetComponent<Inventario> ();
		sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<CenarioController>();
	}
	void Update(){
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("PickDown")) {
			controlScript.lockControl = false;
		} else {
			controlScript.lockControl = true;
		}
	}	

	void OnTriggerStay(Collider player){
		if (item.activeInHierarchy && Input.GetKeyDown (KeyCode.E) && player.gameObject.tag == "Player" && !anim.GetCurrentAnimatorStateInfo (0).IsName("PickDown")) {
			anim.Play("PickDown", -1, 0f);
		    StartCoroutine(Delay(2f));
			Debug.Log ("Pegou item "+nomeItem);
		}
	}

	IEnumerator Delay(float time){
		yield return new WaitForSeconds(time);
		pegaItem ();
		bool check = false;
		for (int i = 0; i < inventario.itensHeal.Count; i++) {
			if (inventario.itensHeal [i].Equals (nomeItem)) {
				check = true;
			}
		}
		if (!check) {
			armazenaItem ();
			sceneController.setItens (nomeItem);
		}
	}
	private void pegaItem(){
		item.SetActive (false);
		audioSource.PlayOneShot (som);
	}
	private void armazenaItem(){
		switch(tipoItem){
			case "Puzzle":
				inventario.setItemPuzzle (nomeItem);
				ItemPuzzleController controllerPuzzle = GameObject.FindGameObjectWithTag ("GameController").GetComponentInChildren<ItemPuzzleController> ();
				controllerPuzzle.SetItemInPanel (nomeItem);
				break;
			case "Heal":
				inventario.setItemHeal(nomeItem); 
				ItemHealController controllerHeal = GameObject.FindGameObjectWithTag ("GameController").GetComponentInChildren<ItemHealController> ();
				controllerHeal.SetItemInPanel ("firstaidkit");
				break;
			case "Weapon":
				ItemWeaponController controllerWeapon = GameObject.FindGameObjectWithTag ("GameController").GetComponentInChildren<ItemWeaponController> ();
				controllerWeapon.SetItemInPanel (nomeItem);
				inventario.setItemWeapon (nomeItem); 
				break;
		}
	}
		

}

