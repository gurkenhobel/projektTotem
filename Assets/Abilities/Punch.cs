using UnityEngine;
using System.Collections;

public class Punch : AttackScript
{

    public override void UseAttack(Transform attackingPlayerTransform)
    {
        //Todo epic shit from here..
        Debug.Log("Punch");

        Vector3 playerPos = attackingPlayerTransform.position;
        Vector3 playerDir = attackingPlayerTransform.forward;

        var opfer = Physics2D.Raycast(playerPos + playerDir, playerDir, 2);
        opfer.rigidbody.AddForce(playerDir * 500);
    }
}
