using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public GameObject Player;
    public GameObject Spawner;
    [SerializeField] GameObject HealthBarUi;
    public int CurrentHealth;
    public int MaxHealth;
    public int level;
    public int AttackDamage;
    public bool IsAlive;
    public int MinimumXpDrop;
    public int MaximumXpDrop;
    public float AttackRange = 5f;
    public float ChaseRange = 10f;
    public float AttackCooldown = 2f;
    public float Speed  = 3f;
    public GameObject ProjectilePrefab;
    public Transform ProjectileSpawnPoint;

    private float lastAttackTime;
    private Transform playerTransform;

    private Rigidbody2D _rigidbody;




    void Start()
    {
        HealthBarUi.SetActive(false);
        IsAlive = true;
        CurrentHealth = MaxHealth;
        playerTransform = Player.transform;
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (!IsAlive) return;

        CheckHealthDropXp();
        ActivateHealthUi();

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= AttackRange && Player.GetComponent<Player>().IsAlive)
        {
            Attack();
        }
        else if (distanceToPlayer <= ChaseRange && Player.GetComponent<Player>().IsAlive)
        {
            ChasePlayer();
        }
    }

    void ActivateHealthUi()
    {
        if (CurrentHealth < MaxHealth)
        {
            HealthBarUi.SetActive(true);
        }
    }

    private void CheckHealthDropXp()
    {
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsAlive = false;
            Player.GetComponent<Player>().AddXp(Random.Range(MinimumXpDrop, MaximumXpDrop));
            if (Spawner != null){
                Spawner.GetComponent<MonsterSpawner>().MonstersOnMap--;
            }
            Destroy(gameObject);
            Debug.Log("Monster is dead");
        }

        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    
    void ChasePlayer()
    {
        if (_rigidbody == null) return;

        // Calculer la direction vers le joueur
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Appliquer une force dans la direction du joueur
        _rigidbody.linearVelocity =direction * 3f;
        
        

        
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= AttackCooldown)
        {
            lastAttackTime = Time.time;
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript == null){
            Debug.Log("!!! cannot get Projectile component iin projectile gameObject");
        }else{
            projectileScript.Initialize(playerTransform.position, AttackDamage  , "Monster" , new Vector2(0,0)); // miguel pour l'attaque damage du monstre
            Debug.Log("Projectile successfully initialized");
        }

        
    }


    

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        
    }

}
