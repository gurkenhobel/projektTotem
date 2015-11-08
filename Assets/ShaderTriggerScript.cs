using UnityEngine;
using System.Collections;

public class ShaderTriggerScript : MonoBehaviour {

    private Shader shader;

    private Material mat;
    
    [SerializeField]
    private Transform totem;

    [SerializeField]
    private Shader wobbly, psychedelic, drunk, reverse;

    // Use this for initialization
    void Start () {
        var t = totem.GetComponent<TotemScript>();
        t.notifyDisplay += (d) => {
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            RenderSettings.ambientIntensity = 1;
            RenderSettings.reflectionIntensity = 1;
            var sun = GameObject.Find("Sun");
            var sunLight = sun.GetComponent<Light>();

            sunLight.enabled = true;

            var players = Transform.FindObjectsOfType<PlayerController>();
            foreach (PlayerController p in players) {
                p.GetComponentInChildren<Light>().enabled = false;
            }

            switch (d) {
                case TotemScript.DisplayModifier.Wobbly:
                    shader = wobbly;
                    break;
                case TotemScript.DisplayModifier.Psychedelic:
                    shader = psychedelic;
                    break;
                case TotemScript.DisplayModifier.Shake:
                    shader = drunk;
                    break;
                case TotemScript.DisplayModifier.Darkness:
                    Camera.main.clearFlags = CameraClearFlags.SolidColor;
                    Camera.main.backgroundColor = Color.black;
                    
                    RenderSettings.ambientIntensity = 0;
                    RenderSettings.reflectionIntensity = 0;
                    foreach (PlayerController p in players) {
                        p.GetComponentInChildren<Light>().enabled = true;
                    }
                    sunLight.enabled = false;
                    break;
                case TotemScript.DisplayModifier.Reverse:
                    shader = reverse;
                    break;
            }
            if (shader != null) mat = new UnityEngine.Material(shader);
            else mat = null;
        };
	}

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (mat != null) Graphics.Blit(source, destination, mat);
        else Graphics.Blit(source, destination);
    }
}
