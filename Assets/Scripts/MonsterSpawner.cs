using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{   
    public GameObject Player;
    public int MonstersLevel;
    public int MinimumHealth;
    public int MaximumHealth;
    public int MinimumAttackDamage;
    public int maximumAttackDamage;
    public string monsterName;

    private int globalMonsterSpawned = 0;

    [SerializeField]int MinimumXpDrop = 1;

    [SerializeField] int MaximumXpDrop = 5;

    [SerializeField] int GlobalMonsterSpawnLimit;

    public int MonstersLimit;

    public GameObject PrefabMonster;

    public float SpawnInterval;
    public bool IsActive = false;

    public  int MonstersOnMap = 0;

    private float _timer;

    private BoxCollider2D bc;
    
    
    void Start(){
        bc = GetComponent<BoxCollider2D>();
    }
    void Update(){
        SpawnMonster();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            IsActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            IsActive = false;
        }
    }
    




    private void SpawnMonster(){
        if (!IsActive){
            return;
        }
        if (MonstersOnMap != MonstersLimit){
            _timer += Time.deltaTime;
        }
        
        if (_timer >= SpawnInterval && MonstersOnMap < MonstersLimit &&   globalMonsterSpawned < GlobalMonsterSpawnLimit){
            GameObject monster = Instantiate(PrefabMonster);
            MonstersOnMap++ ;
            globalMonsterSpawned++;

            
            monster.GetComponent<MonsterStats>().Spawner = gameObject;
            monster.transform.position = new Vector3(
            Random.Range(transform.position.x - (bc.size.x / 2), transform.position.x + (bc.size.x / 2)),
            Random.Range(transform.position.y - (bc.size.y / 2), transform.position.y + (bc.size.y / 2)),
            0);
            

            Canvas monsterCanvas = monster.GetComponentInChildren<Canvas>();
            if (monsterCanvas != null)
            {
                // Attribuer la caméra principale au Canvas en mode World Space
                monsterCanvas.worldCamera = Camera.main;
            }

            MonsterStats stats = monster.GetComponent<MonsterStats>();
            stats.MaxHealth = Random.Range(MinimumHealth,MaximumHealth)*10; //Multiplication pv ennemi x10
            stats.CurrentHealth = stats.MaxHealth;
            stats.AttackDamage = Random.Range(MinimumAttackDamage, maximumAttackDamage)*Player.GetComponent<Player>().Level;
            stats.MinimumXpDrop = MinimumXpDrop;
            stats.MaximumXpDrop = MaximumXpDrop;
            stats.Player = Player;

            _timer = 0;
        }
    }
    


}
