using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveState{
	//PLAYER
	public static void SavePlayerData(Inventario playerItens){
		PlayerData data = new PlayerData ();
		data.itensPuzzleData = new string[playerItens.itensPuzzle.Count];
		data.itensHealData = new string[playerItens.itensHeal.Count];
		data.itensWeaponData = new string[playerItens.itensWeapon.Count];

		for(int i = 0 ; i < playerItens.itensPuzzle.Count;i++){
			data.itensPuzzleData[i] = playerItens.itensPuzzle [i];
		}
		for(int i = 0 ; i < playerItens.itensHeal.Count;i++){
			data.itensHealData[i] = playerItens.itensHeal [i];
		}
		for(int i = 0 ; i < playerItens.itensWeapon.Count;i++){
			data.itensWeaponData[i] = playerItens.itensWeapon [i];
		}

		BinaryFormatter formatter = new BinaryFormatter ();
		string path = Application.persistentDataPath + "/Temp/Player/player.txt";
		FileStream stream = new FileStream (path, FileMode.Create);
		formatter.Serialize (stream, data);
		stream.Close ();
	}
	public static PlayerData LoadPlayerData(){
		
		string path = Application.persistentDataPath + "/Temp/Player/player.txt";
		if (File.Exists (path)) {
			BinaryFormatter formatter = new BinaryFormatter ();
			FileStream stream = new FileStream (path, FileMode.Open);
			PlayerData data = formatter.Deserialize (stream) as PlayerData;
			stream.Close ();
			return data;
		} else {
			Debug.Log ("Arquivo player.txt nao encontrado");
		}
		return null;
	}

	//CENARIO
	public static void SaveSceneData(CenarioController itens){
		CenarioData data = new CenarioData ();
		data.itens = new string[itens.itens.Count];
		data.cena = itens.cena;
		for(int i = 0 ; i < itens.itens.Count;i++){
			data.itens[i] = itens.itens[i];
		}
		BinaryFormatter formatter = new BinaryFormatter ();
		string path = Application.persistentDataPath + "/Temp/Cenario/"+data.cena+".txt";
		FileStream stream = new FileStream (path, FileMode.Create);
		formatter.Serialize (stream, data);
		stream.Close ();
	}

	public static CenarioData LoadSceneData(string cena){

		string path = Application.persistentDataPath + "/Temp/Cenario/"+cena+".txt";
		if (File.Exists (path)) {
			BinaryFormatter formatter = new BinaryFormatter ();
			FileStream stream = new FileStream (path, FileMode.Open);
			CenarioData data = formatter.Deserialize (stream) as CenarioData;
			stream.Close ();
			return data;
		} else {
			Debug.Log ("Arquivo "+cena+".txt nao encontrado");
		}
		return null;
	}



}
