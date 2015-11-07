using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public float health_max = 100f, health_state = 0f;
    public GameObject healthBar;

    // Use this for initialization
    void Start() {
        health_state = health_max;
;    }

    public void decreaseHealth(float value) {
        if (health_state>=0)
        health_state -= 2f * value;
        float health_calculated = health_state / health_max;
        setHealthBar(health_calculated);
    }

    public void increaseHealth(float value) {
        if (health_state<=100)
        health_state += 2f * value;
        float health_calculated = health_state / health_max;
        setHealthBar(health_calculated);
    }

    public void setHealthBar(float myHealth) {
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
