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
        if(Timer <= 0.0F)
        {
            //Todo epic shit from here..
            Debug.Log("Fireball");
            
            //..to here

            //reset Timer
            Timer = coolDownTime;
        }
    }
}
