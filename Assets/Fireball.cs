using UnityEngine;
using System.Collections;

public class Fireball : AttackScript
{
    void Update()
    {
        if (Timer > 0.0F)
        {
            Timer -= Time.deltaTime;
        }
    }

    public override void UseAttack(Transform attackingPlayerTransform)
    {
        Debug.Log("Want to use Fireball");
        if(Timer <= 0.0F)
        {
            //Todo epic shit from here..
            Debug.Log("actually use Fireball");
            
            //..to here

            //reset Timer
            Timer = coolDownTime;
        }
    }
}
