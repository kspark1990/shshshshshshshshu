using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInGameScene : MonoBehaviour {

	public GameObject menu;
	bool b_menu = false;

	private void Awake()
	{
		menu.SetActive(false);
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


}
