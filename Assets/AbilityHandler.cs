using UnityEngine;

public class AbilityHandler : MonoBehaviour {
    public Transform AllAbilities;
    public Transform Totem;
    private AttackScript currentAbility;

    void Start()
    {
        Totem.GetComponent<TotemScript>().notifyAttack += UpdateAbility;
        UpdateAbility(TotemScript.AttackModifier.Fireball);
    }
    
    void Update() {
        if (Input.GetButtonDown("Attack1_" + GetComponent<PlayerController>().InputKey))
        {
            Debug.Log("ATTOCK " + currentAbility.GetType().Name);
            currentAbility.UseAttack(transform);
        }
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
