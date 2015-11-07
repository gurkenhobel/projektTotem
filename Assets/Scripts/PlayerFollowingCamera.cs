using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFollowingCamera : MonoBehaviour {

    public PlayerSpawner playerSpawner;
    Vector3 restingPostition;

	// Use this for initialization
	void Start () {
        restingPostition = transform.position;
        Debug.Log(restingPostition);
	}
	
	// Update is called once per frame
	void Update () {
        List<Vector3> playerPositions = playerSpawner.GetPlayerPositions();

        if (playerPositions.Count == 0)
        {
            transform.position = restingPostition;
            return;
        }

        Vector2 min = Vector2.one * float.MaxValue;
        Vector2 max = Vector2.one * float.MinValue;
        foreach (Vector3 pos in playerPositions)
        {
            if (pos.x < min.x) { min.x = pos.x; }
            if (pos.y < min.y) { min.y = pos.y; }
            if (pos.x > max.x) { max.x = pos.x; }
            if (pos.y > max.y) { max.y = pos.y; }
        }

        min -= Vector2.one * 5F;
        max += Vector2.one * 5F;

        Vector3 average = (Vector3)((min + max) / 2F);
        float depthScale = Mathf.Max(Vector2.Distance(max, min) / 20.0F, 1F);

        Vector3 targetPostition = average + (depthScale - 0.5F) * restingPostition;
        transform.position = Vector3.Lerp(transform.position, targetPostition, 0.1F);
	}
}
