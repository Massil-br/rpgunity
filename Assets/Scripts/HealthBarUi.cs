using TMPro;
using UnityEngine;

public class HealthBarDisplay : MonoBehaviour
{   
    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject HealthText;


    
    float healthBarWidth;
    float backGroundWidth;
    Player playerStats;

    

    private void Start()
    {
        playerStats = Player.GetComponent<Player>();
        backGroundWidth = GetComponentInParent<RectTransform>().rect.width;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBarWidth = backGroundWidth * (playerStats.CurrentHealthPoint / (float)playerStats.MaxHealthPoint);

        RectTransform healthBarRect = GetComponent<RectTransform>();
        healthBarRect.sizeDelta = new Vector2(healthBarWidth, healthBarRect.sizeDelta.y);
        HealthText.GetComponent<TMP_Text>().text = playerStats.CurrentHealthPoint + " / " + playerStats.MaxHealthPoint + " HP";
    }


}
