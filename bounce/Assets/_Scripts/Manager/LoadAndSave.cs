using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAndSave : MonoSingleton<LoadAndSave>
{
	private int reachStage = 10;

	public void SaveReachStage(int _currentReachStage)
	{
		if (_currentReachStage > reachStage)
		{
			PlayerPrefs.SetInt("reachStage", _currentReachStage);
			Debug.Log("reachStage save success! : " + _currentReachStage);
		}
		else
		{
			Debug.Log("reachStage not saved! : " + _currentReachStage);
		}
	}

	public void LoadReachStage()
	{
		reachStage = PlayerPrefs.GetInt("reachStage",10);
		Debug.Log("Load reachStage! : " + reachStage);
	}

	public int GetReachStage()
	{
		return reachStage;
	}

	public void ClearReachStage()
	{
		reachStage = 10;
		PlayerPrefs.SetInt("reachStage", reachStage);
		Debug.Log("clear reachStage data! : " + reachStage);
	}


}
