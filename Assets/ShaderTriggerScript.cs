using UnityEngine;
using System.Collections;

public class ShaderTriggerScript : MonoBehaviour {

    public Shader shader;

    private Material mat;
    
    [SerializeField]
    private Transform totem;

    [SerializeField]
    private Shader wobbly, psychedelic, drunk;

    // Use this for initialization
    void Start () {
        var t = totem.GetComponent<TotemScript>();
        t.notifyDisplay += (d) => {
            Debug.Log(d.ToString());
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
            }
            mat = new UnityEngine.Material(shader);
        };
	}

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (mat != null) Graphics.Blit(source, destination, mat);
        else Graphics.Blit(source, destination);
    }
}
