using UnityEngine;
using System.Collections;

public class DespawnOnContact : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
