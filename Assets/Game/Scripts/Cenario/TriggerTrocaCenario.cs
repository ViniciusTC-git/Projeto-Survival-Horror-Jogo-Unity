using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerTrocaCenario : MonoBehaviour{
	[SerializeField] 
	private string cena = "";//PROXIMA CENA
	public string chave = "chave-185";
	private Inventario inventario;

	void Start(){
		inventario = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventario> ();
	}
	void OnTriggerEnter(Collider player){
			/*if (player.gameObject.tag == "Player" && Input.GetKeyDown (KeyCode.E)) {
				if (inventario.usarItem (chave, "puzzle")) {
					Debug.Log ("ABRIU");
				} else {
					Debug.Log("NÃO TEM CHAVE");
				}
			} */

			CenarioController cenario = GameObject.FindGameObjectWithTag ("GameController").GetComponentInChildren<CenarioController> ();
			Inventario playerItens = player.GetComponent<Inventario> ();
			SaveState.SaveSceneData (cenario);
			SaveState.SavePlayerData (playerItens);
			cenario.LoadScene (cena);
		
	}
}


