using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public TotemScript totem;

    public AudioClip Land;
    public AudioClip Jump;

    AudioSource aus;
    Rigidbody2D rb;

    public string InputKey;

    PlayerHealth ph;

    public Animator animator;

    public const float HorizontalSpeed = 1600f;
    public const float VerticalSpeed = 1600f;
    private int jumpCount = 0;
    private float JumpStart = 0;

    private float lastVInput = 0;

    private Vector2 inputAccel;

    const float MAX_X_SPEED = 500F;

    public bool isDead { get; set; }

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        aus = GetComponent<AudioSource>();
        isDead = false;
    }

    void FixedUpdate() {
        if (!isDead) {
            
        }
    }

    void LateUpdate() {
        if (rb.velocity.x > MAX_X_SPEED) {
            rb.velocity = new Vector2(MAX_X_SPEED, rb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isDead) {
            if (rb.position.y < -6) {
                Debug.Log(gameObject.name + " is out of map.");
                isDead = true;
            }
            
            var hInput = Input.GetAxis("Horizontal_" + InputKey);
            var vInput = Input.GetAxis("Jump_" + InputKey);
            if (hInput != 0) Debug.Log(hInput);
            var startJump = lastVInput == 0 && vInput - lastVInput > 0F;
            lastVInput = vInput;
            var h = HorizontalSpeed * hInput;
            var v = 0F;
            if (jumpCount < 2 && startJump) {
                aus.clip = Jump;
                aus.Play();

                v = VerticalSpeed;
                
                jumpCount++;

                animator.SetBool("Jumping", true);
            }
            
            Vector2 accel = new Vector2(-h, v);

            float delta = Time.deltaTime;
            //rb.position += delta * (rb.velocity + delta * accel / 2);
            rb.velocity = new Vector2(delta * accel.x, rb.velocity.y + delta * accel.y);

            if (Math.Abs(accel.x) < 0.1) rb.velocity = new Vector2(rb.velocity.x * (1 - delta * 32).Clamp(0, 1), rb.velocity.y);

            RotatePlayer();
            if (Math.Abs(rb.velocity.y) < 0.1F) {
                if (jumpCount > 0) {
                    aus.clip = Land;
                    aus.Play();
                }
                jumpCount = 0;

                animator.SetBool("Jumping", false);
            }

            animator.SetBool("Walking", Math.Abs(rb.velocity.x) > 0.3F);
        }
    }

    void RotatePlayer() {
        if (Input.GetAxis("Horizontal_" + InputKey) != 0)
            transform.rotation = Quaternion.Euler(0, -90 * Mathf.Sign(Input.GetAxis("Horizontal_" + InputKey)), 0);
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (totem.bouncy) {
            var other = collider.rigidbody.GetComponent<PlayerController>();
            if (other != null) {
                Debug.Log("Met!");
                var force = transform.position - other.transform.position;
                var ownRB = GetComponent<Rigidbody2D>();
                ownRB.AddForce(force * 600);
            }
        }
    }
}
