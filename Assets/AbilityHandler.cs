using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public Transform AllAbilities;
    public Transform Totem;
    private AttackScript currentAbility;

    public MonoBehaviour GetAbility()
    {
        /*var PfadZumScript = $"{root}/Assets/Abilities/{Player.Name}/{AttackModifier}.set";
        var asset = transform.GetComponent(PfadZumScript);
        Input.Callback()*/
        return null;
    }

    void Start()
    {
        Totem.GetComponent<TotemScript>().notifyAttack += UpdateAbility;
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack1_Keyboard"))
        {
            currentAbility.UseAttack(transform);

        }

    }

    public void UpdateAbility(TotemScript.AttackModifier newState)
    {
        var AttackScripts = AllAbilities.GetComponents<AttackScript>();
        foreach (AttackScript atkScript in AttackScripts)
        {
            if (atkScript.name == newState.ToString())
            {
                currentAbility = atkScript;
                break;
            }

        }

    }
}
