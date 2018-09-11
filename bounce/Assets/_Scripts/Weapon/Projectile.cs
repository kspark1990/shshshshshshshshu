using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	Rigidbody ProjectileRigid;
	Collider ProjectileColl;
	MeshRenderer ProjectileMesh;
	public Gun gun;

	public int bounceCount = 2;
	private bool isTeleport = false;

	ParticleSystem particle;

	private void Awake()
	{
		ProjectileRigid = GetComponent<Rigidbody>();
		ProjectileColl = GetComponent<Collider>();
		ProjectileMesh = GetComponent<MeshRenderer>();
		particle = GetComponent<ParticleSystem>();
		gun = GameManager.Instance.gunScript;
		
		ProjectileRigid.AddForce(gun.transform.forward * gun.force);

	}

	private void DestroyProjectile()
	{
		//before destroy projectile, to maintain trail render 

		//ProjectileRigid.velocity = Vector3.zero;
		//ProjectileColl.enabled = false;
		//ProjectileMesh.enabled = false;


		//detach trail renderer
		this.transform.DetachChildren();
		Destroy(this.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{

		if (collision.transform.tag == "box")
		{
			GameManager.Instance.ChangeBoxCount(-1);
			Destroy(collision.gameObject);
			DestroyProjectile();
			
		}
		else if(collision.transform.tag == "obstacle")
		{
			//when projectile is teleporting, ignore obstacle.
			if (isTeleport == true)
				return;

			if (bounceCount <= 0)
			{
				DestroyProjectile();
			}
			else
			{
				bounceCount--;
				particle.Play();
			}
				
					
		}
	





	}

	private void OnTriggerEnter(Collider other)
	{
		//teleport tag is in,out
		//teleport func is called from Portal script in parent GO
		if (other.transform.tag == "in")
		{
			if (isTeleport == false)
			{
				isTeleport = true;
				Portal p = other.transform.parent.GetComponent<Portal>();
				p.Teleport(true, this.transform.transform);

			}
			//set bool isTeleport false after .5sec
			Invoke("SetTeleport", .5f);
		}
		else if (other.transform.tag == "out")
		{
			if (isTeleport == false)
			{
				isTeleport = true;
				Portal p = other.transform.parent.GetComponent<Portal>();
				p.Teleport(false, this.transform.transform);
			}
			Invoke("SetTeleport", .5f);
		}
	}

	void SetTeleport()
	{
		isTeleport = false;
	}


	private void OnDestroy()
	{
		
		if(gun.currentBulletCount>0)
			gun.reload = true;
		else if(gun.currentBulletCount<=0 && GameManager.Instance.getCurrentBoxCount() > 0)
		{
			GameManager.Instance.GameOver();
		}
				

	}






}
