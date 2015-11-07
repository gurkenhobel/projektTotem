using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public AudioClip Land;
    public AudioClip Jump;

    AudioSource aus;
    Rigidbody2D rb;

    public string InputKey;

    public float HorizontalSpeed = 20.0f;
    public float VerticalSpeed = 400.0f;
    private int jumpCount;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        aus = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = HorizontalSpeed * Input.GetAxis("Horizontal_" + InputKey);

        Vector2 movement = new Vector2(h, 0f);

        if (Input.GetButtonDown("Jump_" + InputKey) && jumpCount < 1)
        {
            aus.clip = Jump;
            aus.Play();


            movement += Vector2.up * VerticalSpeed;
            jumpCount++;
        }

        rb.AddForce(movement);

        if(rb.velocity.y == 0)
        {
            if(jumpCount > 0)
            {
                aus.clip = Land;
                aus.Play();
            }
            jumpCount = 0;
        }
        RotatePlayer();
    }

    void RotatePlayer()
    {
        if (Input.GetAxis("Horizontal_" + InputKey) != 0)
            transform.rotation = Quaternion.Euler(0, 90 * Mathf.Sign(Input.GetAxis("Horizontal_" + InputKey)), 0);
    }
}
