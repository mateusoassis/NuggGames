using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
	[SerializeField]TextMeshProUGUI PauseButtonText;
	[SerializeField]GameObject PausePanel;
	public bool pausedGame;
	
	// iniciar com booleano falso para não pausar o jogo
	void Start()
	{
		pausedGame = false;
	}
	
	// apertou esc = pausa o jogo, ele pausa e despausa no mesmo botão também
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)){
			PauseUnpauseGame();
		}
	}
	
	// lógica para pausar e despausar no mesmo botão
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
