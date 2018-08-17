using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	public GameObject[] stage;
	public int[] boxCount;
	public int[] bulletLimit;


	int currentStageNum = 1;
	int currnetBoxCount;



	private void Awake()
	{
		Screen.SetResolution(1440, 2560, true);

	}


	void ClearStage()
	{


	}

	void GameOver()
	{



	}







}
