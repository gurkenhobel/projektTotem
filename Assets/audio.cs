using UnityEngine;
using System.Collections;

public class audio : MonoBehaviour {
    public AudioSource aus;
    float timeStopped = 0;
	// Use this for initialization
	void Start () {
        aus = GetComponent<AudioSource>();
        aus.Play();
        aus.volume = 0;
        aus.time = 0;
        timeStopped = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (aus.time > aus.clip.length - 2F)
            aus.volume -= 0.5F * Time.deltaTime;
        else
            if ((timeStopped == 0) && (aus.volume < 2F)) aus.volume += 0.5F * Time.deltaTime;
        if (!aus.isPlaying) {
            if (timeStopped == 0) timeStopped = Time.time;
           if (Time.time - timeStopped >= 1F) {
                aus.Play();
                timeStopped = 0;
            }
        }
    }
}
