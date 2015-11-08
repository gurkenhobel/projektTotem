using UnityEngine;

public class AbilityHandler : MonoBehaviour {
    public Transform AllAbilities;
    TotemScript Totem;
    private AttackScript currentAbility;

    public Animator animator;

    float Timer;

    void Start()
    {
        Totem = FindObjectOfType<TotemScript>();
        Totem.notifyAttack += UpdateAbility;
    }
    
    void Update() {
        if (currentAbility == null)
        {
            UpdateAbility(Totem.attack);
        }

        if (Input.GetButtonDown("Attack1_" + GetComponent<PlayerController>().InputKey))
        {
            if (currentAbility != null && Timer >= currentAbility.coolDownTime)
            {
                if (!GetComponent<PlayerController>().isDead)
                {
                    currentAbility.UseAttack(transform, animator);
                }
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
