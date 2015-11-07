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
    private int jumpCount;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        aus = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        bool tooFast = Mathf.Abs(rb.velocity.x) > MaxHorizontalSpeed;
        var input = Input.GetAxis("Horizontal_" + InputKey);
        var h = tooFast ? MaxHorizontalSpeed - (HorizontalSpeed * input) : HorizontalSpeed * input;

        Vector2 movement = new Vector2(-h, 0f);

        if (Input.GetButtonDown("Jump_" + InputKey) && jumpCount == 0) {
            aus.clip = Jump;
            aus.Play();

            movement += Vector2.up * VerticalSpeed;
            jumpCount++;
        }

        Debug.Log("tooFast: " + tooFast);
        Debug.Log("MaxHorizontalSpeed: " + MaxHorizontalSpeed);
        Debug.Log("h: " + h);
        Debug.Log("Speed: " + Mathf.Abs(rb.velocity.x));
        rb.AddForce(new Vector2(Mathf.Sign(movement.x) != Mathf.Sign(rb.velocity.x) ? movement.x * 3 : movement.x, movement.y));

        animator.SetBool("Jumping", true);
        animator.SetBool("Walking", Math.Abs(h) > 0.3F);

        if (rb.velocity.y == 0) {
            if (jumpCount > 0) {
                aus.clip = Land;
                aus.Play();
            }
            jumpCount = 0;

            animator.SetBool("Jumping", false);
        }
    }

    // Update is called once per frame
    void Update() {
        RotatePlayer();
    }

    void RotatePlayer() {
        if (Input.GetAxis("Horizontal_" + InputKey) != 0)
            transform.rotation = Quaternion.Euler(0, -90 * Mathf.Sign(Input.GetAxis("Horizontal_" + InputKey)), 0);
    }
}
