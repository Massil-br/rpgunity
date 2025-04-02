using System.Collections;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] GameObject FireAnimObject;
    
    Animator animator;
    SpriteRenderer spriteRenderer;

    readonly string takeDamageTrigger = "PlayerFireAttack";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = FireAnimObject.GetComponent<Animator>();
        spriteRenderer = FireAnimObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    

    public void PlayFireAttackAnimation()
    {
        spriteRenderer.enabled = true;
        animator.SetTrigger(takeDamageTrigger);
        StartCoroutine(DisableSpriteRendererAfterDelay());
    }

    IEnumerator DisableSpriteRendererAfterDelay()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.3f);

        // Disable the SpriteRenderer
        spriteRenderer.enabled = false;
    }

 


}
