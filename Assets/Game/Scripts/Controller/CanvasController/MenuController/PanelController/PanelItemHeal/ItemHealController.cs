using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;

public class ItemHealController :  ItemBaseController {
	public GameObject[] buttonItem;
	public GameObject[] buttonUsar;
	public GameObject[] buttonDescartar;
	public Transform  panel;

	private Inventario inventario;
	private Text txt;
	void Start(){
		CarregaItens ();
		ConfigButton ();
		inventario = GameObject.FindWithTag ("Player").GetComponent<Inventario> ();
	}
	public override void CarregaItens(){
		PlayerData data = SaveState.LoadPlayerData ();
		bool quantidade = false;
		if (data != null) {
			for (int i = 0; i < data.itensHealData.Length; i++) {
				string filtro = Regex.Replace (data.itensHealData [i], "[0-9]", "");
				Transform heal = panel.FindChild("Itens").FindChild (filtro);
				if (heal) {
					if (quantidade) {
						txt = heal.Find ("quantidade").GetComponentInChildren<Text> ();
						txt.text.Replace ("\n", "");
						txt.text = (System.Convert.ToInt32 (txt.text) + 1).ToString ();		
					} else {
						heal.gameObject.SetActive (true);
						quantidade = true;
					}
				}
			}
		}
	}
	public override void ConfigButton (){
		for(int i = 0; i < buttonItem.Length;i++){
			int index = i;
			buttonItem [i].GetComponent<Button> ().onClick.AddListener(() => actionItem(buttonItem[index].transform));
		}
		for(int i = 0; i < buttonUsar.Length;i++){
			int index = i;
			buttonUsar [i].GetComponent<Button> ().onClick.AddListener(() => actionUsar(buttonUsar[index].transform));
		}
		for(int i = 0; i < buttonDescartar.Length;i++){
			int index = i;
			buttonDescartar [i].GetComponent<Button> ().onClick.AddListener(() => actionDescartar(buttonDescartar[index].transform));
		}
	}
	public override void SetItemInPanel(string nomeItem){
		Transform itens = panel.FindChild ("Itens")
		,item = itens.FindChild ("firstaidkit");

		if (item.gameObject.activeSelf) {
			txt = item.Find ("quantidade").GetComponentInChildren<Text> ();
			txt.text.Replace ("\n", "");
			txt.text = (System.Convert.ToInt32 (txt.text) + 1).ToString ();		
		}

		item.gameObject.SetActive(true);
	}
	public override void actionItem (Transform button)
	{
		if (button.gameObject.activeInHierarchy) {
			if (!button.Find ("Opcoes").gameObject.activeInHierarchy) {
				button.Find ("Opcoes").gameObject.SetActive (true);
			} else {
				button.Find ("Opcoes").gameObject.SetActive (false);
			}
		} 
	}
	public override void actionUsar(Transform button){
		Transform buttonItem = button.parent.parent;
		string nomeItem = buttonItem.name;
		txt = buttonItem.Find ("quantidade").GetComponentInChildren<Text> ();
		int index = System.Convert.ToInt32 (txt.text)-1;
		bool retorno = inventario.usarItem (nomeItem, "heal");

		if (retorno) {
			retorno = inventario.removeItem (index, "heal");
			if (retorno) {
				buttonItem.gameObject.SetActive (false);
			} else {
				txt.text = (System.Convert.ToInt32 (txt.text) - 1).ToString();
			}
		}
	}

	public override void actionDescartar(Transform button){
		Transform buttonItem = button.parent.parent;
		txt = buttonItem.Find ("quantidade").GetComponentInChildren<Text> ();
		int index = System.Convert.ToInt32 (txt.text)-1;
		bool retorno = inventario.removeItem (index, "heal");
		if (retorno) {
			buttonItem.gameObject.SetActive (false);
		} else {
			txt.text = (System.Convert.ToInt32 (txt.text) - 1).ToString();
		}
	}	
}


