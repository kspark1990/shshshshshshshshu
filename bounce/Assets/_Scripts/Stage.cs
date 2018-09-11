using UnityEngine;

public class Stage : MonoBehaviour {

	public GameObject[] _stage;
    public GameObject[] _stageIndexBlock;

    public int[] _boxCount;
	public int[] _bulletLimit;



	private void Awake()
	{

		GameManager.Instance.stage = _stage;
		GameManager.Instance.boxCount = _boxCount;
		GameManager.Instance.bulletLimit = _bulletLimit;
        GameManager.Instance.stageIndexBlock = _stageIndexBlock;



        GameManager.Instance.StageInit();

	}


}
