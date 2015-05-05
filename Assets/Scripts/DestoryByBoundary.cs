using UnityEngine;
using System.Collections;

public class DestoryByBoundary : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerExit (Collider other)
	{
		Destroy (other.gameObject);
	}
}
