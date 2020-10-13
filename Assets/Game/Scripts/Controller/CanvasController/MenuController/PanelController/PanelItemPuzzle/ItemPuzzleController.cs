using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemPuzzleController :  ItemBaseController {
	public GameObject[] buttonItem;
	public GameObject[] buttonUsar;
	public GameObject[] buttonCarousel;
	public Transform  panel;

	//private Inventario inventario;
	private Text txt;

	void Start(){
		CarregaItens ();
		//inventario = GameObject.FindWithTag ("Player").GetComponent<Inventario> ();
		ConfigButton ();
	}
	public override void CarregaItens(){
		PlayerData data = SaveState.LoadPlayerData ();
		if (data != null) {
			for (int i = 0; i < data.itensPuzzleData.Length; i++) {
				Transform puzzle = panel.FindChild ("Itens").FindChild (data.itensPuzzleData [i]);
				if (puzzle) {
					puzzle.gameObject.SetActive (true);
					puzzle.SetAsLastSibling();
					panel.FindChild ("Carousel").gameObject.SetActive (true);
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
		for (int i = 0; i < buttonCarousel.Length; i++) {
			int index = i;
			buttonCarousel [i].GetComponent<Button> ().onClick.AddListener (() => actionCarousel (buttonCarousel [index].transform));
		}

	}
	public override void SetItemInPanel(string nomeItem){
		Transform itens = panel.FindChild ("Itens"),
		item = itens.FindChild (nomeItem);
		item.gameObject.SetActive(true);
		if (!panel.FindChild ("Carousel").gameObject.activeInHierarchy) {
			panel.FindChild ("Carousel").gameObject.SetActive (true);
		}
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
		
		Transform carta = panel.parent.Find ("ButtonCartaBackground");
		if (button.name.Equals("ButtonCartaBackground")) {
			carta.gameObject.SetActive (false);	
			for (int i = 0; i < carta.childCount; i++) {
				if(carta.GetChild (i).gameObject.activeSelf) {
					carta.GetChild (i).gameObject.SetActive (false);
				}	
			}
		} else {
			string item = button.parent.parent.name;
			carta.gameObject.SetActive (true);
			if (item.Equals ("jornal")) {
				carta.Find ("PanelJornal").gameObject.SetActive (true);
			}else if(item.Equals("carta5")) {
				carta.Find ("PanelCarta5").gameObject.SetActive (true);
			}else {
				Transform panelCarta = carta.Find ("PanelCarta");
				Sprite sprite = Resources.Load<Sprite> (item);
				panelCarta.GetComponent<Image> ().sprite = sprite;
				panelCarta.gameObject.SetActive (true);
			}
		}	

	}

	public void actionCarousel(Transform button){
		Transform itens = panel.Find ("Itens");
		Transform itemAtual = itens.GetChild (itens.childCount - 1);
		if (button.name.Equals ("ButtonPrev")) {
			itemAtual.SetAsFirstSibling ();
			for (int i = itens.childCount - 1; i >= 0; i--) {
				Transform item = itens.GetChild (i);
				if (item.gameObject.activeSelf) {
					item.SetAsLastSibling ();
					break;
				}
			}
		
		} else {
			for (int i = 0; i < itens.childCount; i++) {
				Transform item = itens.GetChild (i);
				if (item.gameObject.activeSelf) {
					item.SetAsLastSibling ();
					break;
				}
			}
		}
	}

}

