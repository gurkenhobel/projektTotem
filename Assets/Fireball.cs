using UnityEngine;
using System.Collections;

public class Fireball : AttackScript
{
    public Rigidbody2D FireballPrefab;

    public override void UseAttack(Transform attackingPlayerTransform)
    {
        //Todo epic shit from here..
        Debug.Log("Fireball");

        Vector3 playerPos = attackingPlayerTransform.position;
        Vector3 playerDir = attackingPlayerTransform.forward;

        Rigidbody2D fireBall = (Rigidbody2D)Instantiate(FireballPrefab, playerPos + playerDir * 1.3F, Quaternion.identity);
        fireBall.AddForce(playerDir * 500);

    }
}
