using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;

    public float HorizontalSpeed = 2.0f;
    public float VerticalSpeed = 2.0f;
    private int jumpCount;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = HorizontalSpeed * Input.GetAxis("Horizontal");
        
        Vector2 movement = new Vector2(h, 0f);

        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            movement += Vector2.up * VerticalSpeed;
            jumpCount++;
        }

        rb.AddForce(movement);

        if(rb.velocity.y == 0)
        {
            jumpCount = 0;   
        }
    }
}
