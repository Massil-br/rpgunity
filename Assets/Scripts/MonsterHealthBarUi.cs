using UnityEngine;

public class MonsterHealthBarUi : MonoBehaviour
{
    [SerializeField]
    GameObject Monster;

    

    float healthBarWidth;
    float backGroundWidth;

    MonsterStats monsterStats;
    void Start()
    {
        monsterStats = Monster.GetComponent<MonsterStats>();
        backGroundWidth = GetComponentInParent<RectTransform>().rect.width;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBarWidth = backGroundWidth * (monsterStats.CurrentHealth / (float)monsterStats.MaxHealth);
        GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarWidth, GetComponent<RectTransform>().sizeDelta.y);
    }
}
