using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public float health_max = 100f, health_state = 0f;
    public GameObject healthBar;

    // Use this for initialization
    void Start() {
        health_state = health_max;
    }

    void decreaseHealth() {
        if (health_state>=0)
        health_state -= 2f;
        float health_calculated = health_state / health_max;
        setHealthBar(health_calculated);
    }

    void increaseHealth() {
        if (health_state<=100)
        health_state += 2f;
        float health_calculated = health_state / health_max;
        setHealthBar(health_calculated);
    }

    public void setHealthBar(float myHealth) {
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
