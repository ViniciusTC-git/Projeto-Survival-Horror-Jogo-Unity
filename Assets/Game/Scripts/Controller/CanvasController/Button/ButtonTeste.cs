using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ButtonTeste : MonoBehaviour {

	public void deleteTemp(){
		string path =  Application.persistentDataPath + "/Temp/Cenario";
		if (Directory.Exists (path)) {
			Directory.Delete (path, true);
		}
		Directory.CreateDirectory (path);
		path =  Application.persistentDataPath + "/Temp/Player";
		if (Directory.Exists (path)) {
			Directory.Delete (path, true);
		}
		Directory.CreateDirectory (path);
		Debug.Log ("TEMP LIMPA");
	}
}
