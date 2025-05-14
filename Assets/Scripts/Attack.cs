using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{   
    public float LongDistAttackCoolDown = 1f;
    public float CloseAttackCoolDown = 1f;

    private float _longDistAttackTimer;
    private float _closeAttackTimer;

    private List<GameObject> _targetableMonsters = new List<GameObject>();
    private Player _player;

    public GameObject ProjectilePrefab;
    public Transform ProjectileSpawnPoint; // Position de départ du projectile
    private Rigidbody2D _playerRigidBody;

    void Start()
    {
        _player = GetComponent<Player>();
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster") && !_targetableMonsters.Contains(other.gameObject))
        {
            _targetableMonsters.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            _targetableMonsters.Remove(other.gameObject);
        }
    }

    void Update()
    {   
        CloseAttack();
        LongDistAttack();
    }

    private void CloseAttack()
    {
        if (_closeAttackTimer > 0)
        {
            _closeAttackTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && _closeAttackTimer <= 0)
        {
            _player.GetComponent<PlayerAnimationHandler>().PlayFireAttackAnimation();
            _closeAttackTimer = CloseAttackCoolDown;

            foreach (GameObject target in _targetableMonsters)
            {
                MonsterStats stats = target.GetComponent<MonsterStats>();
                target.GetComponent<MonsterAnimationHandler>().PlayTakeDamageAnimation();
                stats.TakeDamage(_player.AttackDamage); // miguel attack damage clic droit 
            }
        }

        GetComponent<PlayerUiHandler>().UpdateCloseAttackCd(_closeAttackTimer, CloseAttackCoolDown);



    }

    private void LongDistAttack()
    {
        if (_longDistAttackTimer > 0)
        {
            _longDistAttackTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _longDistAttackTimer <= 0)
        {
            _longDistAttackTimer = LongDistAttackCoolDown;
            LaunchProjectile();
        }

        GetComponent<PlayerUiHandler>().UpdateProjectileAttackCD(_longDistAttackTimer, LongDistAttackCoolDown);
    }

    private void LaunchProjectile()
{
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
    Projectile projectileScript = projectile.GetComponent<Projectile>();

    if (projectileScript != null)
    {
        projectileScript.Initialize(mousePosition, _player.AttackDamage, "Player", _playerRigidBody.linearVelocity); // miguel attaque damage du joueur
    }
    else
    {
        Debug.Log("!!! Cannot get Projectile component in projectile GameObject");
    }
}

}
