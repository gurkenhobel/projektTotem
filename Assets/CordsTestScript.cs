using UnityEngine;
using System.Collections;

public class CordsTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown("Jump_Keyboard"))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * Input.GetAxis("Horizontal_Pad1") * 4);
            if(Input.GetButtonDown("Jump_Pad1"))
            {
                Debug.Log("hallo");
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400);
            }
        }
	}
}
