using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public int level;
    public int AttackDamage;
    public bool IsAlive;



    void Update(){
        CheckHealth();
    }






    private void CheckHealth(){
        if (CurrentHealth <= 0){
            CurrentHealth = 0;
            IsAlive = false;
        }

        if (CurrentHealth >= MaxHealth){
            CurrentHealth = MaxHealth;
        }
    }

}
