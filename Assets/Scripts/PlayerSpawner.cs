using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerController PlayerPrefab;
    public bool enableKeyboardPlayer;
    public Transform[] StartPositions;
    bool onlyOnePlayerActive;
    List<PlayerController> currentPlayers = new List<PlayerController>();



	// Use this for initialization
	void Start ()
    {
        RespawnPlayers();
    }
	
    void RespawnPlayers()
    {
        // remove old Players
        for (int i = 0; i < currentPlayers.Count; i++ )
        {
            PlayerController player = currentPlayers[i];
            if (player != null)
                Destroy(player.gameObject);
        }
        currentPlayers.Clear();

        // spawn new players
        for(int i = 0; i < Input.GetJoystickNames().Length; ++i)
        {
            currentPlayers.Add((PlayerController)Instantiate(PlayerPrefab, StartPositions[i].position, Quaternion.identity));
            currentPlayers[currentPlayers.Count - 1].InputKey = "Pad" + (i + 1);
        }
        if (enableKeyboardPlayer)
        {
            currentPlayers.Add((PlayerController)Instantiate(PlayerPrefab, StartPositions[currentPlayers.Count].position, Quaternion.identity));
            currentPlayers[currentPlayers.Count - 1].InputKey = "Keyboard";
        }
        onlyOnePlayerActive = currentPlayers.Count == 1;
    }

	// Update is called once per frame
	void Update ()
    {
        int numDeadPlayers = 0;

        for (int i = currentPlayers.Count - 1; i >= 0; i--)
        {
            PlayerController player = currentPlayers[i];

            if (player == null)
            {
                currentPlayers.RemoveAt(i);
            }
            else if (player.transform.position.y < -100F )
            {
                Destroy(player.gameObject);
                currentPlayers.RemoveAt(i);
            }
            else if (player.isDead)
            {
                numDeadPlayers++;
            }
        }
        if (currentPlayers.Count == numDeadPlayers + 1 && !onlyOnePlayerActive)
        {
            RespawnPlayers();
        }
	}
}









