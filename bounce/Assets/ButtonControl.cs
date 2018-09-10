using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonControl : MonoBehaviour {

	public GameObject[] listButton;

    //set activate button true
	private void SetButtonActive()
	{
        //call load function first before load reachstage data
        LoadAndSave.Instance.LoadReachStage();

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
        Debug.Log(button);
		for(int i = 0; i <= stageIndex; i++)
		{
			button[i].interactable = true;
		}

	}

    private void DeactivateButton()
    {
        for (int i = 0; i < listButton.Length; i++)
        {
            Button[] buttons = listButton[i].GetComponentsInChildren<Button>();
            for (int j = 0; j < buttons.Length; j++)
            {
                buttons[j].interactable = false;
            }
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


    //data clear with Button deactivate.
    public void ClearData()
    {
        LoadAndSave.Instance.ClearReachStage();

        //TODO: Make some UI to make it sure.

        DeactivateButton();
        SetButtonActive();
    }


}
