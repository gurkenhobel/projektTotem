using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public AudioClip Land;
    public AudioClip Jump;

    AudioSource aus;
    Rigidbody2D rb;

    public string InputKey;

    public Animator animator;

    public float HorizontalSpeed = 50.0f;
    public float VerticalSpeed = 400.0f;
    public float MaxHorizontalSpeed = 20;
    private int jumpCount = 0;
    private float JumpStart = 0;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        aus = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        if (rb.position.y < -6) {
            Debug.Log(gameObject.name + " is out of map.");
        }
        bool tooFast = Mathf.Abs(rb.velocity.x) > MaxHorizontalSpeed;
        var input = Input.GetAxis("Horizontal_" + InputKey);
        var h = tooFast ? MaxHorizontalSpeed - (HorizontalSpeed * input) : HorizontalSpeed * input;

        Vector2 movement = new Vector2(-h, 0f);
        if (Input.GetButtonDown("Jump_" + InputKey) && jumpCount < 2) {
            aus.clip = Jump;
            aus.Play();
            movement += Vector2.up * VerticalSpeed;
            jumpCount++;
            animator.SetBool("Jumping", true);
        }

        Debug.Log("tooFast: " + tooFast);
        Debug.Log("MaxHorizontalSpeed: " + MaxHorizontalSpeed);
        Debug.Log("h: " + h);
        Debug.Log("Speed: " + Mathf.Abs(rb.velocity.x));

        if (Mathf.Sign(movement.x) != Math.Sign(rb.velocity.x)) {
            // Faster braking
            rb.AddForce(new Vector2(movement.x * 3, movement.y));
        } else rb.AddForce(movement);

    }

    // Update is called once per frame
    void Update() {
        RotatePlayer();
        if (rb.velocity.y == 0)
        {
            if (jumpCount > 0)
            {
                aus.clip = Land;
                aus.Play();
            }
            jumpCount = 0;

            animator.SetBool("Jumping", false);
        }

        animator.SetBool("Walking", Math.Abs(rb.velocity.x) > 0.3F);
    }

    void RotatePlayer() {
        if (Input.GetAxis("Horizontal_" + InputKey) != 0)
            transform.rotation = Quaternion.Euler(0, -90 * Mathf.Sign(Input.GetAxis("Horizontal_" + InputKey)), 0);
    }
}
