using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterAnimationHandler : MonoBehaviour
{
    private Color originalColor ;

    private float _damageAnimationtimer = 0f;

    [SerializeField] float AnimationTime = 0.2f;

    void Start()
    {
     originalColor = GetComponent<SpriteRenderer>().color;   
    }

    void Update()
    {
        PlayerHurtAnimation();
    }

    // Cette méthode déclenche l'animation de TakeDamage via le trigger
    public void PlayTakeDamageAnimation()
    {
       _damageAnimationtimer = AnimationTime;

    }

    private void PlayerHurtAnimation(){
        if(_damageAnimationtimer >= 0){
            _damageAnimationtimer -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;
        }else{
            GetComponent<SpriteRenderer>().color = originalColor;
        }
    }
}