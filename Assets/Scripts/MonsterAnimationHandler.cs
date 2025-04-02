using UnityEngine;

public class MonsterAnimationHandler : MonoBehaviour
{
    Animator animator;
    readonly string takeDamageTrigger = "TakeDamageTrigger";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Cette méthode déclenche l'animation de TakeDamage via le trigger
    public void PlayTakeDamageAnimation()
    {
        if (animator != null){
            animator.SetTrigger(takeDamageTrigger);
        }
        
        
    }
}