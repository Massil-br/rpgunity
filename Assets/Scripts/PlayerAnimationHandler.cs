using System.Collections;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] GameObject FireAnimObject;
    
    Animator animator;
    SpriteRenderer FireBallspriteRenderer;
    Color basePlayerColor;

    private float _damageAnimationtimer = 0f;

    [SerializeField] float PlayerHurtAnimationTime = 0.2f;

    readonly string takeDamageTrigger = "PlayerFireAttack";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = FireAnimObject.GetComponent<Animator>();
        FireBallspriteRenderer = FireAnimObject.GetComponent<SpriteRenderer>();
        FireBallspriteRenderer.enabled = false;
        basePlayerColor= GetComponent<SpriteRenderer>().color;

    }

    void Update()
    {
        UpdatePlayerHurtAnimation();
    }



    public void PlayFireAttackAnimation()
    {
        FireBallspriteRenderer.enabled = true;
        animator.SetTrigger(takeDamageTrigger);
        StartCoroutine(DisableSpriteRendererAfterDelay());
    }

    IEnumerator DisableSpriteRendererAfterDelay()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.3f);

        // Disable the SpriteRenderer
        FireBallspriteRenderer.enabled = false;
    }
    

    private void UpdatePlayerHurtAnimation(){
       if(_damageAnimationtimer >=0 ){
        _damageAnimationtimer -= Time.deltaTime;
        GetComponent<SpriteRenderer>().color = Color.red;
       }else{
        GetComponent<SpriteRenderer>().color = basePlayerColor;
       }
    }

    public void PlayHurtAnimation(){
        _damageAnimationtimer = PlayerHurtAnimationTime;
    }
 
    

}
