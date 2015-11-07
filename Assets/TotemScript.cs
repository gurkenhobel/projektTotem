using UnityEngine;
using System.Collections;
using System;

public class TotemScript : MonoBehaviour {

    public delegate void TotemChanged<T>(T newState);

    public enum MovementModifier {
        Normal,
        GravityReduction,
        SpeedUp,
        Bounce
    }

    public enum AttackModifier {
        Punch,
        Fireball,
        Soulreaper,
        Reverse,
        Balanced
    }

    public enum DisplayModifier {
        Clear,
        Wobbly,
        Reverse,
        Darkness,
        Shake,
        Psychedelic
    }

    [SerializeField]
    private GameObject top, mid, bottom;

    [SerializeField]
    private float movementTime = 30, attackTime = 60, displayTime = 90;
    private float movementState, attackState, displayState;
    private Vector3 localMovDef, localAttDef, localDisDef;

    public MovementModifier movement { get; private set; }
    public AttackModifier attack { get; private set; }
    public DisplayModifier display { get; private set; }

    public TotemChanged<MovementModifier> notifyMovement;
    public TotemChanged<AttackModifier> notifyAttack;
    public TotemChanged<DisplayModifier> notifyDisplay;

    private T updateEnum<T>(T current) {
        var variants = Enum.GetValues(typeof(T));
        T newVariant = default(T);
        do {
            var index = (int)UnityEngine.Random.Range(0, variants.Length);
            newVariant = (T) variants.GetValue(index);
        } while (current.Equals(newVariant));
        return newVariant;
    }

    private void updateRotation(float state, Transform t, Vector3 def) {
        var x = (float) Math.Max(0, state - 0.8) * 5;
        float ry = (float) Math.Sin(Math.Max(0, x * Math.PI * 4) * 10) * x * x * 30;
        t.localRotation = Quaternion.Euler(def + new Vector3(0, ry, 0));
    }

    private void setTopTexture(Texture albedo) {
        var flag = top.GetComponentsInChildren<MeshRenderer>()[1];
        flag.material.mainTexture = albedo;
    }

    private void setMidTexture(Texture albedo, Texture metallic) {
        var shield = mid.GetComponentsInChildren<MeshRenderer>()[1];
        shield.material.mainTexture = albedo;
        shield.material.SetTexture("metallic", metallic);
    }

    private void setBottomTexture(Texture albedo) {
        var paper = bottom.GetComponentsInChildren<MeshRenderer>()[1];
        paper.material.mainTexture = albedo;
    }
    
	// Use this for initialization
	void Start () {
        movement = MovementModifier.Normal;
        attack = AttackModifier.Punch;
        display = DisplayModifier.Clear;

        localMovDef = top.transform.localRotation.eulerAngles;
        localAttDef = mid.transform.localRotation.eulerAngles;
        localDisDef = bottom.transform.localRotation.eulerAngles;

        notifyMovement += (m) => {
            switch (m) {
                case MovementModifier.GravityReduction:
                    setTopTexture(Resources.Load("gravred_albedo") as Texture);
                    break;
            }
        };

        notifyAttack += (a) => {
            switch (a) {
                case AttackModifier.Fireball:
                    setMidTexture(Resources.Load("fireball_albedo") as Texture, Resources.Load("fireball_metallic") as Texture);
                    break;
            }
        };

        notifyDisplay += (d) => {
            switch (display) {
                case DisplayModifier.Clear:
                    setBottomTexture(Resources.Load("bottom_clear") as Texture);
                    break;
                case DisplayModifier.Psychedelic:
                    setBottomTexture(Resources.Load("psychedelic_albedo") as Texture);
                    break;
                case DisplayModifier.Darkness:
                    setBottomTexture(Resources.Load("darkness_albedo") as Texture);
                    break;
                case DisplayModifier.Shake:
                    setBottomTexture(Resources.Load("drunk_albedo") as Texture);
                    break;
                case DisplayModifier.Wobbly:
                    setBottomTexture(Resources.Load("wobbely_albedo") as Texture);
                    break;
                case DisplayModifier.Reverse:
                    setBottomTexture(Resources.Load("reverse_albedo") as Texture);
                    break;
            }
        };
	}

	// Update is called once per frame
	void Update () {
        var dt = Time.deltaTime;
        movementState += dt / movementTime;
        attackState += dt / attackTime;
        displayState += dt / displayTime;

        if (movementState >= 1F) {
            movement = updateEnum(movement);
            if (notifyMovement != null) notifyMovement(movement);
            movementState = 0F;
        }

        if (attackState >= 1F) {
            attack = updateEnum(attack);
            if (notifyAttack != null) notifyAttack(attack);
            attackState = 0F;
        }

        if (displayState >= 1F) {
            display = updateEnum(display);
            if (notifyDisplay != null) notifyDisplay(display);
            displayState = 0F;
        }

        updateRotation(movementState, top.transform, localMovDef);
        updateRotation(attackState, mid.transform, localAttDef);
        updateRotation(displayState, bottom.transform, localDisDef);
    }
}
