using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float Speed = 8f;
    private Vector2 direction;
    private int damage;
    private string originEntity;

    private float _timer = 0;
    private readonly float _timeToDestroy = 10;
    private Rigidbody2D rb ;



    void Start()
    {
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
            rb.interpolation = RigidbodyInterpolation2D.Interpolate; 
            rb.freezeRotation = true;
    }

    public void Initialize(Vector2 targetPosition, int attackDamage, string origin, Vector2 originVelocity)
    {
        originEntity = origin;
        direction = (targetPosition - (Vector2)transform.position).normalized;
        damage = attackDamage;
        float projectedSpeed = Vector2.Dot(originVelocity, direction);
        Speed += projectedSpeed;
        if (originEntity == "Player"){
            GetComponent<SpriteRenderer>().color = Color.blue;
            gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
        }else if (originEntity == "Monster"){
            gameObject.layer = LayerMask.NameToLayer("MonsterProjectile");
        }else{
            gameObject.layer = LayerMask.NameToLayer("BossProjectile");
        }
        

        
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeToDestroy) 
        {
            Destroy(gameObject);
        }

        
        rb.linearVelocity =direction * Speed;
    }
void HandleCollision(GameObject target)
    {       
        if (originEntity == "Player") {
            if(target.CompareTag("Monster")){
                target.GetComponent<MonsterStats>().TakeDamage(damage);
                Debug.Log($"{target.name} hit! Damage: {damage}");
            }else if(target.CompareTag("Boss")){
                target.GetComponent<BossScript>().TakeDamage(damage);
                Debug.Log($"{target.name} hit! Damage: {damage}");
            }
            Destroy(gameObject);
        }else if ((originEntity == "Monster" || originEntity == "Boss" )&& target.CompareTag("Player")){   
            target.GetComponent<Player>().TakeDamage(damage);
            Debug.Log($"{target.name} hit! Damage: {damage}");
            Destroy(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) => HandleCollision(collision.gameObject);
}
