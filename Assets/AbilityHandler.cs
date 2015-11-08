using UnityEngine;

public class AbilityHandler : MonoBehaviour {
    public Transform AllAbilities;
    public Transform Totem;
    private AttackScript currentAbility;

    public Animator animator;

    float Timer;

    void Start()
    {
        Totem.GetComponent<TotemScript>().notifyAttack += UpdateAbility;
        UpdateAbility(TotemScript.AttackModifier.Fireball);
    }
    
    void Update() {
        if (Input.GetButtonDown("Attack1_" + GetComponent<PlayerController>().InputKey))
        {

            if (Timer >= currentAbility.coolDownTime)
            {
                if(!GetComponent<PlayerController>().isDead)
                    currentAbility.UseAttack(transform,animator);
                Timer = 0.0F;
            }
        }
        Timer += Time.deltaTime;
    }

    public void UpdateAbility(TotemScript.AttackModifier newState) {
        var AttackScripts = AllAbilities.GetComponents<AttackScript>();
        foreach (AttackScript atkScript in AttackScripts) {

            if (atkScript.GetType().Name == newState.ToString())
            {
                currentAbility = atkScript;
                break;
            }
        }
    }
}
