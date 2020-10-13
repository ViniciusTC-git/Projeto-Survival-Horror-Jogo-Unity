using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {
	public bool keyDown = false;
	public List<string> itensPuzzle; 
	public List<string> itensHeal;
	public List<string> itensWeapon; 
	private string maoDireita = "Armature/COG/Body_0/backbone1/backbone2/chest/shoulder_R/arm_R/forearm1_R/forearm2_R/palm_R/mid1_R/Item_R";

	void Start(){
		carregaItens ();
	}
	void Update(){
		/*if (itensWeapon.Count > 0 && Input.GetKeyDown (KeyCode.Q)) {
			if (!keyDown) {
				transform.FindChild (maoDireita).GetChild (0).gameObject.SetActive (true);
				keyDown = true;
			} else {
				transform.FindChild (maoDireita).GetChild (0).gameObject.SetActive (false);
				keyDown = false;
			}
		}*/
	}
	public bool usarItem(string nomeItem,string tipo){
		bool retorno=false;
		switch (tipo) {
			case "weapon":
				equiparWeapon ();
				break;
			case "heal":
				retorno = curarPersonagem (nomeItem);
				break;
			case "puzzle":
				retorno = puzzle (nomeItem);
				break;
		}
		return retorno;
	}
	public void equiparWeapon(){
		if (!keyDown) {
			transform.FindChild (maoDireita).GetChild (0).gameObject.SetActive (true);
			keyDown = true;
		} else {
			transform.FindChild (maoDireita).GetChild (0).gameObject.SetActive (false);
			keyDown = false;
		}
	}
	public bool curarPersonagem(string nomeItem){
		return true;
	}
	public bool puzzle(string nomeItem){
		for (int i = 0; i < itensPuzzle.Count; i++) {
			if (itensPuzzle [i] == nomeItem) {
				return true;
			}
		}
	
		return false;
	}
	public bool removeItem(int index,string tipo){
		bool retorno = false;
		switch(tipo){
			case "weapon":
				itensWeapon.RemoveAt (index);
				break;
		case "heal":
				itensHeal.RemoveAt (index);
				if(itensHeal.Count == 0){
					retorno = true;
				}else{
					retorno = false;
				}
				break;
		}
		return retorno;
	}
		
	void carregaItens(){
		PlayerData data = SaveState.LoadPlayerData ();
		if (data != null) {
			Debug.Log (data);
			for (int i = 0; i < data.itensPuzzleData.Length; i++) {
				itensPuzzle.Add(data.itensHealData[i]);
			}
			for (int i = 0; i < data.itensHealData.Length; i++) {
				itensHeal.Add(data.itensHealData[i]);
			}
			for (int i = 0; i < data.itensWeaponData.Length; i++) {
				itensWeapon.Add(data.itensWeaponData[i]);
			}
		}
	}

	public void setItemHeal(string nome){
		itensHeal.Add(nome);
	}
	public void setItemWeapon(string nome){
		itensWeapon.Add(nome);
	}
	public void setItemPuzzle(string nome){
		itensPuzzle.Add(nome);
	}

}
