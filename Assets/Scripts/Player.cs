using System.Xml.XPath;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsAdmin;
    public int MaxHealthPoint;
    public int CurrentHealthPoint;

    public int AttackDamage;
    public bool IsAlive;
    public int Level;
    public int Xp;
    public int NextLevelXp;
    public float NextLevelRatio;

    private float _healthRegenTimer = 0;
    public float TimeBetweenHealthRegen ;
    public int healthRegenAmount ;

    [SerializeField] bool RegenActivated  = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsAlive = true;
        Level = 1;
    }

    // Update is called once per frame
    void Update()
    {   HealthRegen();
        CheckHealth();
        CheckLevelUp();
        AdminActions();
    }

    private void CheckHealth()
    {
        if (CurrentHealthPoint <= 0)
        {
            CurrentHealthPoint = 0;
            IsAlive = false;
        }

        if (CurrentHealthPoint >= MaxHealthPoint)
        {
            CurrentHealthPoint = MaxHealthPoint;
        }
    }

    private void CheckLevelUp()
    {
        if (Xp >= NextLevelXp)
        {
            Xp -= NextLevelXp;
            Level++;
            NextLevelXp = (int)(NextLevelXp * NextLevelRatio);


            // changer le level up pour choisir entre vitesse/ degats et hp avec une ui sur laquelle on clique

            // Correctly update MaxHealthPoint and CurrentHealthPoint
            int previousMaxHealth = MaxHealthPoint;
            MaxHealthPoint = (int)(MaxHealthPoint * NextLevelRatio);
            CurrentHealthPoint += MaxHealthPoint - previousMaxHealth;

            

            // Correctly update AttackDamage
            AttackDamage = (int)(AttackDamage * NextLevelRatio);

        }

        if (Level >= 20){
            GetComponent<Attack>().CloseAttackCoolDown = 0.5f;
        }


        if (Level >= 15){
            GetComponent<Attack>().LongDistAttackCoolDown = 0.5f;
        }else if (Level >= 10){
            GetComponent<Attack>().LongDistAttackCoolDown = 1f;
        }else if (Level >= 5){
            GetComponent<Attack>().LongDistAttackCoolDown = 1.5f;
        }
    }



    private void HealthRegen(){
        if (!IsAlive || !RegenActivated)return;
        _healthRegenTimer += Time.deltaTime;
        if (_healthRegenTimer >= TimeBetweenHealthRegen){
            CurrentHealthPoint+= healthRegenAmount;
            _healthRegenTimer = 0;
        }

     }

    private void AdminActions()
    {
        if (!IsAdmin)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad1))
        {
            RemoveHealth();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad2))
        {
            GainHealth();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad3))
        {
            GainXp();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad0))
        {
            Revive();
        }
    }

    private void RemoveHealth()
    {
        CurrentHealthPoint--;
    }

    private void GainHealth()
    {
        CurrentHealthPoint++;
    }

    private void Revive()
    {
        CurrentHealthPoint = MaxHealthPoint; // Revive to full health
        IsAlive = true;
    }

    private void GainXp()
    {
        Xp++;
    }


    public void TakeDamage(int damage)
    {
        CurrentHealthPoint -= damage;
        if (CurrentHealthPoint <= 0)
        {
            IsAlive = false;
            Debug.Log("you are dead");
        }
    }
}
