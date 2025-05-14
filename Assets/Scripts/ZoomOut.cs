using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    [SerializeField] Camera Camera;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            Camera.orthographicSize = 7;
        }
    }


}
