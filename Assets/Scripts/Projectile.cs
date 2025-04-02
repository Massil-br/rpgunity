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

    private CircleCollider2D _collider;
    

    public void Initialize(Vector2 targetPosition, int attackDamage, string origin, Vector2 originVelocity)
    {
        originEntity = origin;
        direction = (targetPosition - (Vector2)transform.position).normalized;
        damage = attackDamage;
        float projectedSpeed = Vector2.Dot(originVelocity, direction);
        Speed += projectedSpeed;
        

        _collider = GetComponent<CircleCollider2D>();
        

        if (_collider != null)
        {
            _collider.enabled = false;
            StartCoroutine(EnableColliderAfterDelay(0.2f)); // Active le collider après 0.2s
        }
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_collider != null)
        {
            _collider.enabled = true;
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeToDestroy) 
        {
            Destroy(gameObject);
        }

        transform.position += (Vector3)direction * Speed * Time.deltaTime;
    }

    // Collision avec un objet ayant un Rigidbody (ex: le Player)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) return;

        if (originEntity == "Player" && collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<MonsterStats>().TakeDamage(damage);
            Debug.Log("Monster hit! Damage: " + damage);
            Destroy(gameObject);
        }
        else if (originEntity == "Monster" && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            Debug.Log("Player hit! Damage: " + damage);
            Destroy(gameObject);
        }
        
    }

    // Détection si le projectile entre dans un trigger (ex: trigger du Monster)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (originEntity == "Player" && other.gameObject.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<MonsterStats>().TakeDamage(damage);
            Debug.Log("Monster hit by player projectile! Damage: " + damage);
            Destroy(gameObject);
        }
    }
}
