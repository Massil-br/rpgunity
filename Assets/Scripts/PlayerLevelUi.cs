using TMPro;
using UnityEngine;

public class PlayerLevelUi : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = "LVL. " + Player.GetComponent<Player>().Level;
    }
}
