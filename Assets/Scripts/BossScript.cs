using System.Collections;
using TMPro;
using UnityEngine;

public class BossScript : MonoBehaviour
{   
    [Header("Player")]
    [SerializeField] GameObject Player;
    [Header("Prefabs")]
    [SerializeField] GameObject ProjectilePrefab;


    [Header("UI")]
    [SerializeField] GameObject BossHealthBarBackGround;
    [SerializeField] GameObject BossHealthBar;
    [SerializeField] GameObject BossHealthBarText;
    [SerializeField] string BossName;

    [Header("Stats")]
    [SerializeField] bool isAlive = true;
    [SerializeField] int CurrentHealthPoint = 1200;
    [SerializeField] int MaxHealthPoint = 1200;
    [SerializeField] int AttackDamage = 8;

    [Header("Attack Path Durations")]

    [SerializeField] float Step1Duration = 10; // attack aiming to player every 0.5s // AimingToPlayerCoolDown
    [SerializeField] float Step2Duration = 5; // do nothing
    [SerializeField] float Step3Duration = 10; // launch to all directions at the sameTme every 1s // AllDirectionAttackCoolDown
    [SerializeField] float Step4Duration = 5; // do nothing 

    [SerializeField] float Step5Duration = 4; // circle progressive Launch  360° in duration time
    [SerializeField] float Step6Duration = 1; //do nothing 
    [SerializeField] float Step7Duration = 4; // circle progressive Launch  360° in duration time
    [SerializeField] float Step8Duration = 6; // do nothing 

    [Header("CoolDowns")]
    [SerializeField] float AimingToPlayerCoolDown = 0.5f;
    [SerializeField] float AllDirectionAttackCoolDown  = 1f;

    [SerializeField] int CircleAttackProjectileNumber = 2;

    [Header("LaunchScript")]
    [SerializeField]public bool StartBossScript = false;

    public DeathPopup death;
    
    int pathStep = 0;
    //Timers
    float stepsTimer = 0f;
    float AimingToPlayerTimer =0f;
    float AllDirectionAttackTimer = 0f;
    
    //All Direction Attack Directions
    Vector2[] directions = new Vector2[]{
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right,
        new Vector2(1,1).normalized,
        new Vector2(-1,1).normalized,
        new Vector2(1,-1).normalized,
        new Vector2(-1,-1).normalized,
    };

   



    RectTransform healthBarRect;

    float healthBarWidth;
    float backGroundWidth;

    void Start()
    {
        BossHealthBarBackGround.SetActive(false);
        backGroundWidth = BossHealthBarBackGround.GetComponent<RectTransform>().rect.width;
        healthBarRect = BossHealthBar.GetComponent<RectTransform>();
        healthBarWidth = healthBarRect.rect.width;

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
    {   
        if (StartBossScript){
            BossHealthBarBackGround.SetActive(true);
            SetMonsterName();
            DrawMonsterHealthBar();
        }

        if(isAlive){
            CheckHealth();
            HandlePathSteps();
        }
    }

    

    void CheckHealth(){
        
        if (CurrentHealthPoint <= 0){
            CurrentHealthPoint = 0;
            BossDead();
        }
    }

    void BossDead(){
        isAlive = false;
        death.DeathScreen("GG\n\nYou beat the\ndemo of this game !");
    }


    void AimingToPlayerAttack(){
        GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript == null){
            Debug.Log("!!! cannot get Projectile component iin projectile gameObject");
        }else{
            projectileScript.Initialize(Player.transform.position, AttackDamage  , "Boss" , new Vector2(0,0));
            Debug.Log("Projectile successfully initialized");
        }

    }

    void AllDirectionAttack(){
        for(int i =0 ; i< directions.Length; i++){
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(new Vector2 (transform.position.x +directions[i].x, transform.position.y + directions[i].y), AttackDamage  , "Boss" , new Vector2(0,0));
        }
    }

    IEnumerator CircleAttack(float duration){
        float delay =  duration / CircleAttackProjectileNumber;
        for (int i = 0; i < CircleAttackProjectileNumber; i++){
            float angle = i * (360f/CircleAttackProjectileNumber);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 direction  = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(new Vector2 (transform.position.x +direction.x,transform.position.y + direction.y), AttackDamage, "Boss", Vector2.zero);
            yield return new WaitForSeconds(delay);
        }
        
    }


    void HandlePathSteps(){
      if (!StartBossScript){
        pathStep = 0;
        stepsTimer = 0;
      }else{
        stepsTimer += Time.deltaTime;
        switch (pathStep){
            case 0 :
                stepsTimer = 0;
                pathStep = 1;
                break;
            case 1:

                if(stepsTimer >= Step1Duration){
                    pathStep = 2;
                    AimingToPlayerTimer = 0;
                    stepsTimer = 0;
                    break;
                }

                if (AimingToPlayerTimer <= 0){
                    AimingToPlayerAttack();
                    AimingToPlayerTimer = AimingToPlayerCoolDown;
                }else{
                    AimingToPlayerTimer -= Time.deltaTime;
                }

                break;
            case 2:
                if(stepsTimer >= Step2Duration){
                    pathStep = 3;
                    stepsTimer = 0;
                }
                break;
            case 3:
                if(stepsTimer >= Step3Duration){
                    pathStep = 4;
                    stepsTimer = 0;
                    break;
                }
                if (AllDirectionAttackTimer <= 0){
                    AllDirectionAttack();
                    AllDirectionAttackTimer = AllDirectionAttackCoolDown;
                }else{
                    AllDirectionAttackTimer -= Time.deltaTime;
                }
                break;
            case 4:
                if (stepsTimer >= Step4Duration){
                    pathStep = 5;
                    stepsTimer = 0;
                }
                break;
            case 5:
                StartCoroutine(CircleAttackStep(6,Step5Duration));
                pathStep = -1;
                
                break;
            case 6:
                if (stepsTimer >=Step6Duration){
                    pathStep = 7;
                    stepsTimer = 0;
                }
                break;
            case 7:
                StartCoroutine(CircleAttackStep(8,Step7Duration));
                pathStep = -1;
                
                break;
            case 8:
                if(stepsTimer >= Step8Duration){
                    pathStep = 0;
                    stepsTimer = 0;
                }
                break;

            }
            
            
      }
    }

    IEnumerator CircleAttackStep(int nextStep, float duration)
    {
        yield return StartCoroutine(CircleAttack(duration));
        pathStep = nextStep;
        stepsTimer = 0;
    }

    public void TakeDamage(int damage)
    {
        int trueDamage = Mathf.CeilToInt(damage*UnityEngine.Random.Range(0.75f,1.26f)*10); //formule variation dmg sur les mobs
        if (trueDamage > 60){
            trueDamage = 60;
        }
        Debug.Log(CurrentHealthPoint);
        Debug.Log(trueDamage);
        CurrentHealthPoint -= trueDamage;
        Debug.Log(CurrentHealthPoint);
        GetComponent<MonsterAnimationHandler>().PlayTakeDamageAnimation();
    }

    



    void SetMonsterName(){
        BossHealthBarText.GetComponent<TMP_Text>().SetText(BossName);
    }

    void DrawMonsterHealthBar(){
        healthBarWidth = backGroundWidth * (CurrentHealthPoint / (float)MaxHealthPoint);
        healthBarRect.sizeDelta = new Vector2(healthBarWidth, healthBarRect.sizeDelta.y);
    }


}
