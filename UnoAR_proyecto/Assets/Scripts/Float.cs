using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class Float : MonoBehaviour 
{
	float originalZ;


	void Start()
	{
		this.originalZ = this.transform.position.z;
	}

	void Update()
	{
		transform.position = new Vector3(transform.position.x,
			transform.position.y, 
			originalZ + -(Math.Abs((float)Math.Sin(Time.time)))*0.2f);
	}

}
