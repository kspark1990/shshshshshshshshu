using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUI : MonoBehaviour {

	public GameObject[] bullet;



	public void ResetBulletUI()
	{
		for (int i = 0; i < bullet.Length; i++)
		{
			bullet[i].gameObject.SetActive(false);
		}
	}


	public void SetBulletCount(int count)
	{
		
		for(int i =0; i<count; i++)
		{
			bullet[i].gameObject.SetActive(true);
		}

	}


	public void AddBulletCount(int currentCount, int addCount)
	{
		for(int i = currentCount-1; i<currentCount+addCount; i++)
		{
			bullet[i].gameObject.SetActive(true);


			if(i >= bullet.Length-1)
			{
				break;
			}
		}


	}


	public void Shoot(int count)
	{
		bullet[count].gameObject.SetActive(false);


	}




}
