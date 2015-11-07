using UnityEngine;
using System.Collections;

public class ExplosionForceOnContact : MonoBehaviour
{
    public float ExplosionForce = 3000F;
    public float ExplosionRadius = 10F;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("EXPLOSION");

        Vector2 explosionPos = transform.position;

        Collider2D[] opfers = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (Collider2D opfer in opfers)
        {
            Vector2 opferPos = opfer.transform.position;

            Vector2 explosionToOpfer = opferPos - explosionPos;
            float distance = explosionToOpfer.magnitude;

            if (distance < ExplosionRadius)
            {
                opfer.attachedRigidbody.AddForce(explosionToOpfer.normalized * ExplosionForce * Mathf.Lerp(1F,0F, distance / ExplosionRadius));

                PlayerHealth opferHealth = opfer.GetComponent<PlayerHealth>();
                if (opferHealth != null)
                {
                    float damage = 20;
                    opferHealth.health_state -= damage * Mathf.Lerp(1F, 0F, distance / ExplosionRadius);
                }
            }
        }

        Destroy(gameObject);
    }
}
