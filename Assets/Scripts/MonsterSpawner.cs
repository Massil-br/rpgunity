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
        _timer += Time.deltaTime;
        if (_timer >= SpawnInterval && MonstersOnMap < MonstersLimit){
            GameObject monster = Instantiate(PrefabMonster);
            MonstersOnMap++ ;
            monster.GetComponent<MonsterStats>().Spawner = gameObject;
            monster.transform.position = new Vector3(Random.Range(transform.position.x-(bc.size.x/2),transform.position.x+(bc.size.x/2)),
            Random.Range(transform.position.y-(bc.size.x/2),transform.position.y+(bc.size.x/2)),0);


            Canvas monsterCanvas = monster.GetComponentInChildren<Canvas>();
            if (monsterCanvas != null)
            {
                // Attribuer la caméra principale au Canvas en mode World Space
                monsterCanvas.worldCamera = Camera.main;
            }

            MonsterStats stats = monster.GetComponent<MonsterStats>();
            stats.MaxHealth = Random.Range(MinimumHealth,MaximumHealth)*10; //Multiplication pv ennemi x10
            stats.CurrentHealth = stats.MaxHealth;
            stats.AttackDamage = Random.Range(MinimumAttackDamage, maximumAttackDamage);
            stats.Player = Player;

            _timer = 0;
        }
    }
    


}
