using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoSingleton<SceneController>
{
	public int GetSceneIndex()
	{
		return SceneManager.GetActiveScene().buildIndex;
	}
	
	public void LoadScene(int sceneIndex, int stageIndex)
	{
		SceneManager.LoadScene(sceneIndex);
		GameManager.Instance.SetCurrentStageNum(stageIndex);
	}
	//public void LoadMoveStageFirst(int index)
	//{
	//	GameManager.Instance.MoveStage(index);
	//}




}
