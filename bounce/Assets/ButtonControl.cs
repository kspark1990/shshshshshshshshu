using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonControl : MonoBehaviour {

	public GameObject[] listButton;

	private void SetButtonActive()
	{
		int reachStage = LoadAndSave.Instance.GetReachStage();
		int sceneIndex = reachStage / 10;
		int stageIndex = reachStage % 10;

		//scene is 1 to ~
		for (int i = 0; i < sceneIndex-1; i++)
		{
			Button[] buttons = listButton[i].GetComponentsInChildren<Button>();
			for(int j = 0; j < buttons.Length; j++)
			{
				buttons[j].interactable = true;
			}
		}
		//
		Button[] button = listButton[sceneIndex - 1].GetComponentsInChildren<Button>();
		for(int i = 0; i <= stageIndex; i++)
		{
			button[i].interactable = true;
		}





	}


	private void Awake()
	{
		SetButtonActive();



	}

	//param should be more then 10
	public void LobbyToStage(int scene)
	{
		int stageIndex = scene % 10;
		scene /= 10;
		SceneController.Instance.LoadScene(scene,stageIndex);
	}

	public void LoadReachStage()
	{
		int reachStage = LoadAndSave.Instance.GetReachStage();
		LobbyToStage(reachStage);
	}




}
