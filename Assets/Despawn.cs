using UnityEngine;
using System.Collections;

public class Despawn : MonoBehaviour {

    public float LifeTime = 10.0F;

	// Update is called once per frame
	void Update () {
        LifeTime -= Time.deltaTime;

	}
}
