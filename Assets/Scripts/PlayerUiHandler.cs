using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerUiHandler : MonoBehaviour
{
    [SerializeField] GameObject  HealthDamageUi;
    [SerializeField] GameObject  XpDropUi;

    [SerializeField] GameObject LevelUpUi;

    [SerializeField] GameObject CloseAttackUiCD;
    [SerializeField] GameObject ProjectileAttackUiCD;


    [SerializeField] float DisplayTime = 1f;

    [SerializeField] GameObject AttackDamageTextUi;

    private float _healthDamageUiTimer   = 0f;
    private float _xpDropUiTimer = 0f;
    private float _levelUpUiTimer = 0f;

    

    
    public void UpdateCloseAttackCd(float closeAttackTimer, float coolDownTime){
        if (closeAttackTimer <= 0){
            CloseAttackUiCD.GetComponent<UnityEngine.UI.Image>().fillAmount  = 1;
            CloseAttackUiCD.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            
        }else{
            CloseAttackUiCD.GetComponent<UnityEngine.UI.Image>().fillAmount = 1 - (closeAttackTimer / coolDownTime);
            CloseAttackUiCD.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
    }

    public void UpdateProjectileAttackCD(float projectileAttackTimer, float coolDownTime){
        if (projectileAttackTimer <= 0){
            ProjectileAttackUiCD.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
            ProjectileAttackUiCD.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }else{
           ProjectileAttackUiCD.GetComponent<UnityEngine.UI.Image>().fillAmount = 1 - (projectileAttackTimer / coolDownTime);
           ProjectileAttackUiCD.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
    }
    
    
    void Start()
    {
        HealthDamageUi.GetComponent<TMP_Text>().color = Color.red;
        XpDropUi.GetComponent<TMP_Text>().color = Color.blue;
        LevelUpUi.GetComponent<TMP_Text>().color = Color.white;
        CloseAttackUiCD.GetComponent<UnityEngine.UI.Image>().fillAmount  = 1;
        ProjectileAttackUiCD.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
        
    }

    void Update()
    {
        UpdateHealthTimer();
        UpdateXpTimer();
        UpdateLevelUpTimer();
        UpdateAttackDamageUi();
  
    }

    public void ShowXpDropGained(int amount){
        XpDropUi.GetComponent<TMP_Text>().SetText($"+{amount} XP");
        _xpDropUiTimer = DisplayTime;
    }

    public void ShowLevelUp(){
        LevelUpUi.GetComponent<TMP_Text>().SetText($"LVL UP!");
        _levelUpUiTimer = DisplayTime;
    }


    public void ShowHealthDamageTaken(int damage){
        HealthDamageUi.GetComponent<TMP_Text>().SetText($"-{damage} HP");
        _healthDamageUiTimer = DisplayTime;
    }
    
    private void UpdateHealthTimer(){
        if(_healthDamageUiTimer >= 0){
            _healthDamageUiTimer -= Time.deltaTime;
            HealthDamageUi.SetActive(true);
        }else {
            HealthDamageUi.SetActive(false);
        }
    }

    private void UpdateXpTimer(){
        if (_xpDropUiTimer  >= 0){
            _xpDropUiTimer -= Time.deltaTime;
            XpDropUi.SetActive(true);
        }else {
            XpDropUi.SetActive(false);
        }

    }


    private void UpdateLevelUpTimer(){
        if(_levelUpUiTimer >=  0){
            _levelUpUiTimer -= Time.deltaTime;
            LevelUpUi.SetActive(true);
        }else{
            LevelUpUi.SetActive(false);
        }

    }

    private void UpdateAttackDamageUi(){
        AttackDamageTextUi.GetComponent<TMP_Text>().SetText($"{GetComponent<Player>().AttackDamage}");
    }


    
    
}
