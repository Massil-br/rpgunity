using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] GameObject Boss;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            Camera.orthographicSize = 7;
            Boss.GetComponent<BossScript>().StartBossScript = true;
        }
    }


}
