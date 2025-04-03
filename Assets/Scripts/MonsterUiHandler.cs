using TMPro;
using UnityEngine;

public class MonsterUiHandler : MonoBehaviour
{
    [SerializeField] GameObject DamageTakenUi; 
    [SerializeField] float DisplayTime = 1f;

    private float _damageTakenUiTimer = 0f;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DamageTakenUi.GetComponent<TMP_Text>().color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDamageTakenUi();
    }


    public void ShowDamageTaken(int damage){
        DamageTakenUi.GetComponent<TMP_Text>().SetText($"-{damage} HP");
        _damageTakenUiTimer = DisplayTime;
    }

    private void UpdateDamageTakenUi(){
        if (_damageTakenUiTimer >= 0){
            _damageTakenUiTimer -= Time.deltaTime;
            DamageTakenUi.SetActive(true);
        }else{
            DamageTakenUi.SetActive(false);
        }
    }


}
