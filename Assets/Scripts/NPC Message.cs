using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCMessage : MonoBehaviour
{
    [SerializeField] GameObject TopLeftInterract;
    [SerializeField] GameObject TopLeftMessageUI;
    [SerializeField] string InterractMessage;
   

    [SerializeField] GameObject ConversationUi;
   
    
    [SerializeField]GameObject ConversationUiMsg;
    

    [SerializeField]string ConvMessage;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TopLeftInterract.SetActive(false);
        ConversationUi.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        TopLeftInterract.SetActive(true);
        TopLeftMessageUI.GetComponent<TMP_Text>().text = InterractMessage;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        TopLeftInterract.SetActive(false);
        ConversationUi.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (TopLeftInterract.activeSelf){
            if (Input.GetKeyDown(KeyCode.E)){
                if (!ConversationUi.activeSelf){
                    ConversationUi.SetActive(true);
                    
                }else{
                    ConversationUi.SetActive(false);
                }
                
                ConversationUiMsg.GetComponent<TMP_Text>().text = ConvMessage;
            }
        }
    }
}
