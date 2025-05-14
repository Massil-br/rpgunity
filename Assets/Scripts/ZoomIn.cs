using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    [SerializeField] Camera Camera;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            Camera.orthographicSize = 5;
        }
    }


}
