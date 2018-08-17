using UnityEngine;

public class Stage : MonoBehaviour {

	public GameObject[] _stage;
	public int[] _boxCount;
	public int[] _bulletLimit;

	Transform trans;


	private void Awake()
	{
		GameManager.Instance.stage = _stage;
		GameManager.Instance.boxCount = _boxCount;
		GameManager.Instance.bulletLimit = _bulletLimit;

	}


}
