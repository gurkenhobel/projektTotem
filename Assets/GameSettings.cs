using UnityEngine;

public class GameSettings : MonoBehaviour {

    public bool KeyboardPlayerEnabled { get; set; }

	// Use this for initialization
    void Awake() {
      DontDestroyOnLoad(this);
    }

	void Start () {
        KeyboardPlayerEnabled = false;
	}

    public void UpdateGameController() {
        GetComponent<PlayerSpawner>().enableKeyboardPlayer = KeyboardPlayerEnabled;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdatePlayerEnable() {
        KeyboardPlayerEnabled = GetComponent<UnityEngine.UI.Toggle>().isOn;
    }
}
