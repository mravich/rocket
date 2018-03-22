using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistManagers : MonoBehaviour {
	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);

	}
}
