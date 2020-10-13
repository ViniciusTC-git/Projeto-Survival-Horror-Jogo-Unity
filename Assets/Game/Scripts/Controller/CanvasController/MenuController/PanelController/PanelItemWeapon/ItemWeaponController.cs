using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemWeaponController :  ItemBaseController {
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
		if (data != null) {
			for (int i = 0; i < data.itensWeaponData.Length; i++) {
				Transform weapon = panel.FindChild ("Itens").FindChild (data.itensWeaponData [i]);
				if (weapon) {
					weapon.gameObject.SetActive (true);
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
		Transform itens = panel.FindChild ("Itens"),
		item = itens.FindChild (nomeItem);
		//itens = panel.FindChild ("Itens");

		/*GameObject obj = Instantiate (panel.gameObject);
		obj.name = nomeItem + (tamanho + 1);
		obj.transform.SetParent (itens.transform, false);
		obj.transform.SetSiblingIndex (1);*/

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
		string nomeItem = buttonItem.name,
		panel = buttonItem.parent.parent.name;
		inventario.usarItem (nomeItem,"weapon");
		txt = button.GetComponentInChildren<Text> ();
		if (txt.text.Replace("\n","").Equals("Equipar")) {
			txt.text = "Remover";
		}else{
			txt.text = "Equipar";
		}	
	}

	public override void actionDescartar(Transform button){
		Transform buttonItem = button.parent.parent;
		string panel = buttonItem.parent.parent.name;
		inventario.removeItem (0,"weapon");
		buttonItem.gameObject.SetActive (false);	
	}

}

