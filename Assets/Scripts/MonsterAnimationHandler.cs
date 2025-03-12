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
        // Si l'animation TakeDamage n'est pas déjà en cours, on active le trigger
        animator.SetTrigger(takeDamageTrigger);
    }
}