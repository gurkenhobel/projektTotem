using UnityEngine;
using System.Collections;

public class DancerRotation : MonoBehaviour {

    public Transform transform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0.1f, 0));
	}
}
