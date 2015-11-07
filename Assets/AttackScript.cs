using UnityEngine;
using System.Collections;

public abstract class AttackScript : MonoBehaviour {

    public float coolDownTime;
    protected float Timer;

    public virtual void UseAttack(Transform attackingPlayerTransform)
    {
        
    }
}
