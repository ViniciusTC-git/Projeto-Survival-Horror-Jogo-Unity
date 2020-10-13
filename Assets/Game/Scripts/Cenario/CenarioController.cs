using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CenarioController : MonoBehaviour {
	public static string prevScene = "";
	public static string currentScene = "";
	public List<string> itens;
	public string cena;

	public virtual void Start() {
		currentScene = SceneManager.GetActiveScene().name;
		cena =  SceneManager.GetActiveScene().name;
		LoadSceneState ();
	}
	public void LoadScene(string proximaCena) {
		prevScene = currentScene;
		SceneManager.LoadScene (proximaCena);
	}
	void LoadSceneState(){
		CenarioData data = SaveState.LoadSceneData (cena);
		GameObject itens = GameObject.FindGameObjectWithTag ("Itens");
		if (data != null) {
			Debug.Log (data);
			if (data.itens.Length != 0) {
				for (int i = 0; i < data.itens.Length; i++) {
					setItens(data.itens [i]);
					for (int j = 0; j < itens.transform.childCount; j++) {
						if (itens.transform.GetChild (j).FindChild (data.itens [i])) {
							itens.transform.GetChild (j).gameObject.SetActive (false);
						}
					}
					Debug.Log (data.itens [i]);
				}
			} else {
				Debug.Log ("Nenhum item");
			}

		}
	}

	public void setItens(string nome){
		itens.Add (nome);
	}

}
