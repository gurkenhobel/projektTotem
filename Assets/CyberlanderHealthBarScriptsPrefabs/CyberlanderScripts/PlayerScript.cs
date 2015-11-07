using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public float speed;

    // Update is called once per frame
    void Update() {
        handleMovement();
    }

    private void handleMovement() {
        float translation = speed * Time.deltaTime;
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
    }

    void OnTriggerStay(Collider other) {
        if (other.name == "Health") {
            Debug.Log("Getting Healed");
        } else if (other.name == "Damage") {
            Debug.Log("Getting Damaged");
        }
    }
}
