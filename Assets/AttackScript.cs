using UnityEngine;
using System.Collections;

public abstract class AttackScript : MonoBehaviour {

    public float coolDownTime;

    public virtual void UseAttack(Transform attackingPlayerTransform)
    {
        
    }
}
