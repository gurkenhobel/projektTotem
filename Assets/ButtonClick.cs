using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeScene(int i) {
        Application.LoadLevel(i);
    }

    public void ExitScene() {
        Application.CancelQuit();
    }
}
