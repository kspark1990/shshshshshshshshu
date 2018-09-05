using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	public GameObject[] stage;
	public int[] boxCount;
	public int[] bulletLimit;

	


	int currentStageNum = 0;
	int currnetBoxCount;


	//for instansiate stuff; 
	//using Resources.Load
	GameObject gunPrefab;
	GameObject bulletPrefab;

	//
	GameObject gun;
	public Gun gunScript;

	//get stage index from other class
	public int getCurrentStageNum()
	{
		return currentStageNum;
	}
	public int getStageBulletLimit()
	{
		return bulletLimit[currentStageNum];
	}
	public GameObject getStageGO()
	{
		return stage[currentStageNum];
	}

	public void StageInit()
	{
		currnetBoxCount = boxCount[currentStageNum];
		SpawnGun();
		MoveCamera();
	}

	//this method is called in first scene
	public override void Init()
	{
		base.Init();
		LoadGunPrefabs();
		LoadBulletPrefabs();
		//load Reachstage
		LoadAndSave.Instance.LoadReachStage();



	}

	void LoadGunPrefabs()
	{
		gunPrefab = Resources.Load("Prefabs/" + "Weapon/" + "gun") as GameObject;
		if (gunPrefab == null)
		{
			Debug.Log("gun load failed");
		}
		
	}

	void SpawnGun()
	{
		Transform gunTrans = getStageGO().transform.GetChild(0);
		if (!gunTrans.CompareTag("Shooter"))
			Debug.Log("shooter load falied!");


		//Instantiate gun GO with stage data
		gun = Instantiate(gunPrefab, gunTrans.position, gunTrans.rotation);

		gunScript = gun.GetComponent<Gun>();
		//set gun bullet count
	}

	public void LoadBulletPrefabs()
	{
		bulletPrefab = Resources.Load("Prefabs/" + "Weapon/" + "Bullet") as GameObject;
		if (bulletPrefab == null)
		{
			Debug.Log("bullet load failed");
		}
	}

	public void InstantiateBullet(Transform muzzle)
	{
		Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);
	}


	//test
	public void MoveCamera()
	{
		Transform trans = getStageGO().transform;
		Camera.main.transform.position = trans.position;
	}
	public void SetCurrentStageNum(int index)
	{
		currentStageNum = index;
	}

	//int param means stage num
	public void MoveStage(int stageIndex)
	{
		//TODO : if stage is 10, load lobby scene
		currentStageNum += stageIndex;

		if (currentStageNum == 10)
		{
			//TODO : change scene 
		}


		Transform trans = getStageGO().transform;
		//set gun pos, rot
		gun.transform.position = trans.GetChild(0).position;
		gun.transform.rotation = Quaternion.Euler(0, 0, 0);
		//set gun bullet count
		gunScript.currentBulletCount = getStageBulletLimit();
		//move camera position
		Camera.main.transform.position = trans.position;

		currnetBoxCount = boxCount[currentStageNum];

		gunScript.ResetbulletUI();
		gunScript.nowChangingStage = false;
	}






	public void ChangeBoxCount(int count)
	{
		currnetBoxCount += count;

		if(currnetBoxCount <=0)
		{
			ClearStage();
		}

	}



	void ClearStage()
	{
		gunScript.nowChangingStage = true;
		MoveStage(1);
		LoadAndSave.Instance.SaveReachStage(SceneController.Instance.GetSceneIndex() * 10 + currentStageNum);
	}

	void GameOver()
	{


	}







}
