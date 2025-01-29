using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed ;

    private Rigidbody2D _rb;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX,moveY)*Speed;
        _rb.linearVelocity = movement;
    }
}
