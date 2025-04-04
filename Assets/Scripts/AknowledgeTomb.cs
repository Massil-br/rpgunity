using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AknowledgeTomb : MonoBehaviour
{
    [SerializeField] GameObject TopLeftInterract;
    [SerializeField] GameObject TopLeftMessage;

    [SerializeField] string InterractMessage;

    [SerializeField]GameObject Player;

    private bool CanKillPlayer = false;

    

    // Start is calle   d once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TopLeftInterract.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TopLeftInterract.activeSelf &&Input.GetKeyDown(KeyCode.E) && CanKillPlayer){
            
            Player.GetComponent<Player>().TakeDamage(Player.GetComponent<Player>().MaxHealthPoint);
           
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player")){
            TopLeftInterract.SetActive(true);
            TopLeftMessage.GetComponent<TMP_Text>().SetText(InterractMessage);
            CanKillPlayer = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player")){
              TopLeftInterract.SetActive(false);
              CanKillPlayer = false;
        }
    }
      
}
