﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMcaller : MonoBehaviour {

	private void Awake()
	{
		GameManager.Instance.Init();
	}
}
