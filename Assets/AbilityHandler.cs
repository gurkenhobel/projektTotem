using UnityEngine;

public class AbilityHandler : MonoBehaviour {
    public Transform AllAbilities;
    public Transform Totem;
    private AttackScript currentAbility;

    float Timer;

    void Start()
    {
        Totem.GetComponent<TotemScript>().notifyAttack += UpdateAbility;
        UpdateAbility(TotemScript.AttackModifier.Punch);
    }
    
    void Update() {
        if (Input.GetButtonDown("Attack1_" + GetComponent<PlayerController>().InputKey))
        {

            if (Timer >= currentAbility.coolDownTime)
            {
                currentAbility.UseAttack(transform);
                Timer = 0.0F;
            }
        }
        Timer += Time.deltaTime;
    }

    public void UpdateAbility(TotemScript.AttackModifier newState) {
        var AttackScripts = AllAbilities.GetComponents<AttackScript>();
        foreach (AttackScript atkScript in AttackScripts) {
            Debug.Log(atkScript.GetType().Name + " " + newState.ToString());
            if (atkScript.GetType().Name == newState.ToString())
            {
                currentAbility = atkScript;
                break;
            }
        }
    }
}
