using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
	[SerializeField]TextMeshProUGUI PauseButtonText;
	[SerializeField]GameObject PausePanel;
	public bool pausedGame;
	
	void Start()
	{
		pausedGame = false;
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)){
			PauseUnpauseGame();
		}
	}
	
    public void PauseUnpauseGame()
	{
		if(!pausedGame){
			Time.timeScale = 0f;
			PauseButtonText.text = "Unpause";
			PausePanel.SetActive(true);
			pausedGame = true;
		} else if(pausedGame){
			Time.timeScale = 1f;
			PauseButtonText.text = "Pause";
			PausePanel.SetActive(false);
			pausedGame = false;
		}		
	}	
}
