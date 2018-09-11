using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInGameScene : MonoBehaviour {

	public GameObject menu;
    public GameObject lastStageMenu;
	bool b_menu = false;

	private void Awake()
	{
		menu.SetActive(false);

        if (lastStageMenu != null)
            lastStageMenu.SetActive(false);
        else
            Debug.Log("assign LastStageMenu GO!!");
	}



	public void OpenMenu()
	{
		if (!b_menu)
		{
			menu.SetActive(true);
			b_menu = true;
		}
		else
		{
			menu.SetActive(false);
			b_menu = false;
		}
	}


	public void ReturnToTitle()
	{
		SceneController.Instance.ReturnToTitle();
	}

    public void ActiveLastStageMenu()
    {
        lastStageMenu.SetActive(true);
    }

    //this function is called by lastStageMenu Button.
    public void GoTONextStage()
    {
        SceneController.Instance.LoadScene(SceneController.Instance.GetSceneIndex() + 1, 0);
    }
}
