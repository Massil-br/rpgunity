using UnityEngine;

public class MonsterStats : MonoBehaviour
{   
    public GameObject Player;

    public GameObject Spawner;

    [SerializeField]
    GameObject HealthBarUi;

    public int CurrentHealth;
    public int MaxHealth;
    public int level;
    public int AttackDamage;
    public bool IsAlive;

    public int MinimumXpDrop;
    public int MaximumXpDrop;

    void Start()
    {
        HealthBarUi.SetActive(false);
    }

    void Update(){
        CheckHealthDropXp();
        ActivateHealthUi();
    }


    void ActivateHealthUi(){
        if (CurrentHealth < MaxHealth){
            HealthBarUi.SetActive(true);
        }
    }






    private void CheckHealthDropXp(){
        if (CurrentHealth <= 0){
            CurrentHealth = 0;
            IsAlive = false;
            Player.GetComponent<Player>().Xp += Random.Range(MinimumXpDrop,MaximumXpDrop);
            Spawner.GetComponent<MonsterSpawner>().MonstersOnMap --;
            Destroy(gameObject);
        }

        if (CurrentHealth >= MaxHealth){
            CurrentHealth = MaxHealth;
        }
    }

}
