using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	public GameObject[] stage;
    public GameObject[] stageIndexBlock;
	public int[] boxCount;
	public int[] bulletLimit;

	int currentStageNum = 0;
	int currnetBoxCount;

	bool emptyBullet = false;

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
    public GameObject getStageIndexBlockGO()
    {
        return stageIndexBlock[currentStageNum];
    }

    public int getCurrentBoxCount()
	{
		return currnetBoxCount;
	}

	public void StageInit()
	{
		currnetBoxCount = boxCount[currentStageNum];
		SpawnGun();
        StartCoroutine(MoveCam());
		//MoveCamera();
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
    //not using now
	public void MoveCamera()
	{
		Transform trans = getStageGO().transform;
		Camera.main.transform.position = trans.position;
	}


	public void SetCurrentStageNum(int index)
	{
		currentStageNum = index;
	}

	//int param means stage num(1 ~ 10)
	public void MoveStage(int stageIndex)
	{
		//TODO : if stage is 10, load lobby scene
		currentStageNum += stageIndex;
		LoadAndSave.Instance.SaveReachStage(SceneController.Instance.GetSceneIndex() * 10 + currentStageNum);

        //if stage is over 10(means clear current scene)
		if (currentStageNum >= 10)
		{
            //found script in Hierarchy
            //TODO : find better way to find that script.
            ButtonInGameScene BGC = FindObjectOfType<ButtonInGameScene>();
            if (BGC == null)
                Debug.Log("ButtonInGameScene Load Failed!!");
            else
                BGC.ActiveLastStageMenu();

			return;
		}


        StartCoroutine(MoveStuff());

		
	}

    //this method is call when load stage from SceneController.cs in LoadScene method
    IEnumerator MoveCam()
    {
        gunScript.nowChangingStage = true;
        //TODO : need change!!
        gunScript.bulletUI.ResetBulletUI();
        Transform trans = getStageGO().transform;
        if (getStageIndexBlockGO() != null)
            Camera.main.transform.position = getStageIndexBlockGO().transform.position;
        else
            Camera.main.transform.position = trans.position;

        yield return new WaitForSeconds(.4f);
        while ((Camera.main.transform.position - trans.position).magnitude >= .01f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, trans.position, Time.deltaTime * 5f);
            yield return null;
        }
        //TODO : find better way
        gunScript.bulletUI.SetBulletCount(getStageBulletLimit());
        gunScript.nowChangingStage = false;

    }


    //include gun, camera moving
    //cam moving animation
    IEnumerator MoveStuff()
    {
        Transform trans = getStageGO().transform;
        //set gun pos, rot
        gun.transform.position = trans.GetChild(0).position;
        gun.transform.rotation = Quaternion.Euler(0, 0, 0);
        //set gun bullet count
        //TODO : check it!
        gunScript.bulletUI.ResetBulletUI();

        gunScript.currentBulletCount = getStageBulletLimit();

        //move camera position
        Vector3 midPos = (trans.position + Camera.main.transform.position)/2;
        while ((Camera.main.transform.position - midPos).magnitude >= .01f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, midPos,Time.deltaTime * 5f);
            yield return null;
        }
        while ((Camera.main.transform.position - trans.position).magnitude >= .01f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, trans.position, Time.deltaTime * 5f);
            yield return null;
        }

        Camera.main.transform.position = trans.position;
        currnetBoxCount = boxCount[currentStageNum];
        gunScript.ResetbulletUI();
        gunScript.nowChangingStage = false;
        yield return null;
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
	}

	public void GameOver()
	{
		Debug.Log("GAME OVER");
		SceneController.Instance.ReturnToTitle();
	}







}
