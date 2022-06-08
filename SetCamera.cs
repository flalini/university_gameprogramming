using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
	void Awake()
	{
		Screen.SetResolution(
			Screen.resolutions[0].width,
			Screen.resolutions[0].height,
			true);
	}
}
