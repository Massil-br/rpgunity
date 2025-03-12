using TMPro;
using UnityEngine;

public class XpBarDisplay : MonoBehaviour
{   
    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject XpText;

    
    float xpBarWidth;
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
        xpBarWidth = backGroundWidth * (playerStats.Xp / (float)playerStats.NextLevelXp);

        RectTransform healthBarRect = GetComponent<RectTransform>();
        healthBarRect.sizeDelta = new Vector2(xpBarWidth, healthBarRect.sizeDelta.y);
        XpText.GetComponent<TMP_Text>().text = playerStats.Xp + " / " + playerStats.NextLevelXp +" XP";
    }


}
