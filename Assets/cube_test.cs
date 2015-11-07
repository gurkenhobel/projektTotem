using UnityEngine;
using System.Collections;

public class cube_test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate() {
        if (GetComponent<Rigidbody2D>().angularVelocity > 0) GetComponent<Rigidbody2D>().AddTorque(-0.1F);
        if ((GetComponent<Rigidbody2D>().angularVelocity < 100F) && (Input.GetButton("Fire1"))) GetComponent<Rigidbody2D>().AddTorque(100F);
    }
}
