using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour {
	
	public GameObject menuItem;
	void Start(){
		menuItem.SetActive(false);
	}
		
	void Update(){
		if(Input.GetKeyDown (KeyCode.I)) 
		{
			if (!menuItem.activeInHierarchy) {
				PauseGame();
			}
			else{
				ContinueGame(); 
			}
		} 
	}
	void PauseGame()
	{
		Time.timeScale = 0;
		menuItem.SetActive(true);
	} 
	void ContinueGame()
	{
		Time.timeScale = 1;
		menuItem.SetActive(false);
	}




}
