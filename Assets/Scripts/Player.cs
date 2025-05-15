using UnityEngine;


public class Player : MonoBehaviour
{
    public bool IsAdmin;
    public int MaxHealthPoint;
    public int MaxHealthPointCap = 150;
    public int CurrentHealthPoint;

    public int AttackDamage;
    public bool IsAlive;
    public int Level;
    public int LevelCap = 20;
    public int Xp;
    public int NextLevelXp;
    public float NextLevelRatio;

    private float _healthRegenTimer = 0;
    public float TimeBetweenHealthRegen ;
    public int healthRegenAmount ;

    public float gameTime = 0f;
    public bool isGameRunning = true;

    private Vector3 initialPosition;

    private PlayerUiHandler playerUiHandler;

    public GameModePopup popup;
    public FadeImage fade;
    public DeathPopup death;

    [SerializeField] bool RegenActivated  = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsAlive = true;
        Level = 1;
        playerUiHandler = GetComponent<PlayerUiHandler>();
        initialPosition = transform.position;

        if (popup == null)
        {
            popup = FindFirstObjectByType<GameModePopup>();
        }
        if (popup == null){
            Debug.Log("Popup Skill issue");
        }

        if (fade == null)
        {
            fade = FindFirstObjectByType<FadeImage>();
        }
        if (fade == null){
            Debug.Log("Fade Skill issue");
        }

        if (death == null)
        {
            death = FindFirstObjectByType<DeathPopup>();
        }
        if (death == null){
            Debug.Log("Death Skill issue");
        }
    }

    // Update is called once per frame
    void Update()
    {   HealthRegen();
        CheckHealth();
        CheckLevelUp();
        AdminActions();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isGameRunning = false;
            popup.ShowPopup("The game is\npaused ! \n\n Use this time to\ntouch some grass !");
        }
        if (isGameRunning)
        {
            gameTime += Time.deltaTime;
        }
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
            playerUiHandler.ShowLevelUp();

            //Lvl up system
            if(Level%2 == 0){
                if(MaxHealthPoint < MaxHealthPointCap){
                    int previousMaxHealth = MaxHealthPoint;
                    MaxHealthPoint += 10;
                    CurrentHealthPoint += MaxHealthPoint - previousMaxHealth;
                }
            }else{
                AttackDamage += 2;
            }

        }

        if(GetComponent<Cheat>().konami == false){
            switch (Level) {
                case 5 : GetComponent<Attack>().LongDistAttackCoolDown = 1f; break;

                case 10 : GetComponent<Attack>().LongDistAttackCoolDown = 0.75f; break;

                case 15 : GetComponent<Attack>().LongDistAttackCoolDown = 0.5f; break;

                case 20 : GetComponent<Attack>().CloseAttackCoolDown = 0.5f; break;
            }
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


    public void AddXp(int amount){
        Xp += amount;
        playerUiHandler.ShowXpDropGained(amount);
    }


    public void TakeDamage(int damage)
    {
        int trueDamage = Mathf.CeilToInt(damage*UnityEngine.Random.Range(0.5f,1.6f)); // Formule variation dmg sur le joueur
        CurrentHealthPoint -= trueDamage; 
        playerUiHandler.ShowHealthDamageTaken(trueDamage);
        GetComponent<PlayerAnimationHandler>().PlayHurtAnimation();
        
        if (CurrentHealthPoint <= 0)
        {
           PlayerDead(); 
        }
    }


    public void PlayerDead(){
        isGameRunning = false;
        IsAlive = false;
        Debug.Log("you are dead");
        fade.Skill_Issue();
    }

    public void Retry(){
        Revive();
        transform.position = initialPosition;
    }
}
