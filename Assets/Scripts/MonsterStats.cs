using UnityEngine;

public class MonsterStats : MonoBehaviour
{   
    public GameObject Player;
    public int CurrentHealth;
    public int MaxHealth;
    public int level;
    public int AttackDamage;
    public bool IsAlive;

    public int MinimumXpDrop;
    public int MaximumXpDrop;



    void Update(){
        CheckHealthDropXp();
    }






    private void CheckHealthDropXp(){
        if (CurrentHealth <= 0){
            CurrentHealth = 0;
            IsAlive = false;
            Player.GetComponent<Player>().Xp += Random.Range(MinimumXpDrop,MaximumXpDrop);
            Destroy(gameObject);
        }

        if (CurrentHealth >= MaxHealth){
            CurrentHealth = MaxHealth;
        }
    }

}
