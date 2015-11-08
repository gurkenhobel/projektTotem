using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    public List<PlayerController> PlayerPrefab;
    public bool enableKeyboardPlayer;
    public Transform[] StartPositions;
    bool onlyOnePlayerActive;
    List<PlayerController> currentPlayers = new List<PlayerController>();

    public List<Vector3> GetLivingPlayerPositions()
    {
        List<Vector3> pPos = new List<Vector3>();
        foreach (PlayerController pC in currentPlayers)
            if(!pC.isDead)
                pPos.Add(pC.transform.position);
        return pPos;
    }

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
        foreach (string joystickName in Input.GetJoystickNames())
        {
            if(joystickName != "")
            {
                Debug.Log("Spawning " + joystickName + "-Player");

                int playerCount = currentPlayers.Count;

                currentPlayers.Add((PlayerController)Instantiate(PlayerPrefab[playerCount - 1], StartPositions[playerCount].position, Quaternion.identity));
                currentPlayers[currentPlayers.Count - 1].InputKey = "Pad" + (playerCount + 1);
            }
        }
        if (enableKeyboardPlayer)
        {
            Debug.Log("Spawning Keyboard-Player");

            currentPlayers.Add((PlayerController)Instantiate(PlayerPrefab[currentPlayers.Count - 1], StartPositions[currentPlayers.Count].position, Quaternion.identity));
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
            else if (player.isDead)
            {
                numDeadPlayers++;
            }
        }
        if (!onlyOnePlayerActive && currentPlayers.Count <= numDeadPlayers + 1
            || onlyOnePlayerActive && currentPlayers.Count <= numDeadPlayers)
        {
            RespawnPlayers();
        }
	}
}









