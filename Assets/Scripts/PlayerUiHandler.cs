using TMPro;
using UnityEngine;

public class PlayerUiHandler : MonoBehaviour
{
    [SerializeField] GameObject  HealthDamageUi;
    [SerializeField] GameObject  XpDropUi;

    [SerializeField] GameObject LevelUpUi;


    private float _healthDamageUiTimer   = 0f;
    private float _xpDropUiTimer = 0f;
    private float _levelUpUiTimer = 0f;

    [SerializeField] float DisplayTime = 1f;

    

    
    
    void Start()
    {
        HealthDamageUi.GetComponent<TMP_Text>().color = Color.red;
        XpDropUi.GetComponent<TMP_Text>().color = Color.blue;
        LevelUpUi.GetComponent<TMP_Text>().color = Color.white;
        
    }

    void Update()
    {
        
        if(_healthDamageUiTimer >= 0){
            _healthDamageUiTimer -= Time.deltaTime;
            HealthDamageUi.SetActive(true);
        }else {
            HealthDamageUi.SetActive(false);
        }

        if (_xpDropUiTimer  >= 0){
            _xpDropUiTimer -= Time.deltaTime;
            XpDropUi.SetActive(true);
        }else {
            XpDropUi.SetActive(false);
        }

        if(_levelUpUiTimer >=  0){
            _levelUpUiTimer -= Time.deltaTime;
            LevelUpUi.SetActive(true);
        }else{
            LevelUpUi.SetActive(false);
        }



    }

    public void ShowXpDropGained(int amount){
        XpDropUi.GetComponent<TMP_Text>().SetText($"+{amount} XP");
        _xpDropUiTimer = DisplayTime;
    }

    public void ShowLevelUp(){
        LevelUpUi.GetComponent<TMP_Text>().SetText($"LVL ⇑");
        _levelUpUiTimer = DisplayTime;
    }


    public void ShowHealthDamageTaken(int damage){
        HealthDamageUi.GetComponent<TMP_Text>().SetText($"-{damage} HP");
        _healthDamageUiTimer = DisplayTime;
    }

    
}
