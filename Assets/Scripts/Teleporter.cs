using TMPro;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField]GameObject TopLeftInterract;
    [SerializeField]GameObject TopLeftMessage;
    [SerializeField]string TpZoneName;

    [SerializeField]GameObject OtherTeleporter;
    private bool _teleporterMsg = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TopLeftInterract.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_teleporterMsg && Input.GetKeyDown(KeyCode.E)){
            Player.transform.position = OtherTeleporter.transform.position;
            Player.transform.position += new Vector3(0,1,0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        TopLeftInterract.SetActive(true);
        _teleporterMsg = true;
        TopLeftMessage.GetComponent<TMP_Text>().SetText($"Press E To Teleport To {TpZoneName}");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        TopLeftInterract.SetActive(false);
        _teleporterMsg = false;        
    }



}
