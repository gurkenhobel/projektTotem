using UnityEngine;

public abstract class AttackScript : MonoBehaviour {

    public float coolDownTime;

    public virtual void UseAttack(Transform attackingPlayerTransform)
    {
        
    }

    public virtual void UseAttack(Transform attackingPlayerTransform, Animator attackingPlayerAnimator)
    {

    }
}
