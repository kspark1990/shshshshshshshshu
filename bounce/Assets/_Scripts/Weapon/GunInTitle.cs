using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInTitle : MonoBehaviour {

	public float speed = 30f;
	private float rot = 0;
	bool isRight = true;

	public int maximumBulletCount;
	public	GameObject bulletGO;
	Queue<GameObject> bulletQueue = new Queue<GameObject>();

	public float force = 10f;
	public Transform muzzle;

	void RotateGun()
	{
		if (isRight)
			rot += speed * Time.deltaTime;
		else
			rot -= speed * Time.deltaTime;

		if (rot > 70)
			isRight = false;
		else if (rot < -70)
			isRight = true;

		transform.rotation = Quaternion.Euler(0, rot, 0);
	}

	void Shot()
	{
		GameObject GO = Instantiate(bulletGO, muzzle.transform.position, muzzle.transform.rotation);
		if (bulletQueue.Count == maximumBulletCount)
			 Destroy(bulletQueue.Dequeue());
		bulletQueue.Enqueue(GO);
		GO.GetComponent<Rigidbody>().AddForce(muzzle.forward * force);
	}

	// Update is called once per frame
	void Update () {
		RotateGun();

		if (Input.GetMouseButtonDown(0))
		{
			Shot();
		}

	}
}
