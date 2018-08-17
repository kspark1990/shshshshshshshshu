using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {



	Vector3 aimVector;

	public float force = 10f;
	public Transform muzzle;
	public GameObject bullet;

	Camera viewCamera;

	public bool reload = true;

	public BulletUI bulletUI;
	public int currentBulletCount = 7;
	public int maxBulletCount = 10;

	private void Awake()
	{
		viewCamera = Camera.main;
		bulletUI.ResetBulletUI();
		bulletUI.SetBulletCount(currentBulletCount);
	}


	void Aim()
	{
		if (Input.GetMouseButton(0))
		{
			if (TouchPosition().z <= this.transform.position.z)
			{
				Debug.Log("클릭");

				Vector3 aVector = TouchPosition() - this.transform.position;
				Vector3 bVector3 = new Vector3(0, 0, TouchPosition().z) - this.transform.position;

				float theta = Vector3.Dot(aVector, bVector3) / (aVector.magnitude * bVector3.magnitude);
				float angle = Mathf.Acos(theta) * Mathf.Rad2Deg;

				Vector3 dirAngle = Vector3.Cross(aVector, bVector3);
				if (dirAngle.y > 0.0f)
					angle = -angle;


				if (angle <= -70)
				{
					angle = -70;
				}
				if (angle >= 70)
				{
					angle = 70;
				}


				transform.rotation = Quaternion.Euler(0, angle, 0);


			}
			//else
				//transform.LookAt(new Vector3(0, 0, 0));
		}
		else
		{
			Debug.Log("클릭 X");
		}
	}
	Vector3 TouchPosition()
	{

		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayDistance;

		if (groundPlane.Raycast(ray, out rayDistance))
		{
			Vector3 point = ray.GetPoint(rayDistance);
			return new Vector3(point.x, 0, point.z);
		}
		else
			return new Vector3(0, 0, -5);

	}

	void Shot()
	{
		GameObject go = Instantiate(bullet, muzzle.position, muzzle.rotation);
		Projectile projectile = go.GetComponent<Projectile>();
		projectile.gun = this.GetComponent<Gun>();

		Rigidbody bulletRigid = go.GetComponent<Rigidbody>();
		bulletRigid.AddForce(go.transform.forward * force);
		reload = false;

		currentBulletCount--;
		bulletUI.Shoot(currentBulletCount);


	}

	//게임메니저로 이동
	void AddBullet(int count)
	{
		bulletUI.AddBulletCount(currentBulletCount, count);

		currentBulletCount += count;
		if (currentBulletCount >= maxBulletCount)
		{
			currentBulletCount = maxBulletCount;
		}

	}


	private void Update()
	{
		Aim();

		if (Input.GetMouseButtonUp(0)&&reload)
		{
			Shot();
		}

		if (Input.GetKeyDown(KeyCode.F1))
		{
			AddBullet(3);
		}


	}
}
