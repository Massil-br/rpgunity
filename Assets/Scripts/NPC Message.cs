using TMPro;
using UnityEngine;

public class NPCMessage : MonoBehaviour
{
    [SerializeField] GameObject TopLeftInterract;
    [SerializeField] GameObject TopLeftMessageUI;
    [SerializeField] string InterractMessage;
   

    [SerializeField] GameObject ConversationUi;
   
    
    [SerializeField]GameObject ConversationUiMsg;
    

    [SerializeField]string ConvMessage;

    private bool ActiveNpcMessage = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TopLeftInterract.SetActive(false);
        ConversationUi.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {   
        ActiveNpcMessage = true;
        TopLeftInterract.SetActive(true);
        TopLeftMessageUI.GetComponent<TMP_Text>().text = InterractMessage;
    }

    void OnTriggerExit2D(Collider2D collision)
    {   
        ActiveNpcMessage = false;
        TopLeftInterract.SetActive(false);
        ConversationUi.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (TopLeftInterract.activeSelf && Input.GetKeyDown(KeyCode.E) && ActiveNpcMessage){
                if (!ConversationUi.activeSelf){
                    ConversationUi.SetActive(true);
                    
                }else{
                    ConversationUi.SetActive(false);
                }
                ConversationUiMsg.GetComponent<TMP_Text>().text = ConvMessage;
        }
    }
}
