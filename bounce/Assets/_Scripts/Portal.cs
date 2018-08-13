using UnityEngine;

public class Portal : MonoBehaviour {

	public GameObject portalIN;
	public GameObject portalOUT;

	//float offsetZ is for accurate teleport position
	public void Teleport(bool IN,Transform projectile)
	{
		if(IN)
		{
			float offsetZ = portalIN.transform.position.z - projectile.transform.position.z;
			projectile.transform.position = portalOUT.transform.position - new Vector3(0, 0, offsetZ);
		}

		else
		{
			float offsetZ = portalOUT.transform.position.z - projectile.transform.position.z;
			projectile.transform.position = portalIN.transform.position - new Vector3(0, 0, offsetZ); 
		}
			
	}
}
