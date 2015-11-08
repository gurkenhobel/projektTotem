using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    public TotemScript totem;
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
                int playerCount = currentPlayers.Count;

                PlayerController player = (PlayerController)Instantiate(PlayerPrefab[currentPlayers.Count], StartPositions[playerCount].position, Quaternion.identity);
                player.InputKey = "Pad" + (playerCount + 1);
                currentPlayers.Add(player);

                Debug.Log("Spawning " + joystickName + "-Player, listening to " + player.InputKey);
            }
        }
        if (enableKeyboardPlayer)
        {
            PlayerController player = (PlayerController)Instantiate(PlayerPrefab[currentPlayers.Count], StartPositions[currentPlayers.Count].position, Quaternion.identity);
            player.InputKey = "Keyboard";
            currentPlayers.Add(player);

            Debug.Log("Spawning Keyboard-Player, listening to " + player.InputKey);
        }
        onlyOnePlayerActive = currentPlayers.Count == 1;

        if (totem != null)
        {
            if(totem.notifyMovement != null)
                totem.notifyMovement(totem.movement);
            if (totem.notifyAttack != null)
                totem.notifyAttack(totem.attack);
            if (totem.notifyDisplay != null)
                totem.notifyDisplay(totem.display);
        }
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









