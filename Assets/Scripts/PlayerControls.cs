using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed ;

    private Rigidbody2D _rb;
    SpriteRenderer playerSpriteRenderer;
    public Sprite[] PlayerIdle;
    public Sprite[] Playerhorizontal;
    public Sprite[] PlayerUp;
    public Sprite[] PlayerDown;

    public Sprite[] PlayerDead;
    private Sprite[] currentAnimation ;
    private Sprite[] previousAnimation ;
     private float animationTimer = 0f;
    private float animationSpeed = 0.1f; // Vitesse de l'animation en secondes par frame
    private int currentFrame = 0;
    

    void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveX,moveY).normalized*Speed;
        PlayerAnimation(Time.deltaTime, movement);
        if (GetComponent<Player>().IsAlive){
            _rb.linearVelocity = movement;
        }
        
    }



    private void PlayerAnimation(float deltaTime, Vector2 movement)
        {
            // Déterminer quelle animation utiliser
            if (!GetComponent<Player>().IsAlive)
            {
                currentAnimation = PlayerDead;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) )
            {
                currentAnimation = Playerhorizontal;
                if(Input.GetKey(KeyCode.A)){
                    playerSpriteRenderer.flipX = false;
                }else{
                    playerSpriteRenderer.flipX = true;
                }
            }
            else if (Input.GetKey(KeyCode.S)){
                currentAnimation = PlayerDown;
                
            }else if(Input.GetKey(KeyCode.W )){
                currentAnimation = PlayerUp;
            }
            else
            {   
                if (movement == new Vector2(0,0)){
                     currentAnimation = PlayerIdle;
                }
               
            }

            // Réinitialiser la frame si l'animation a changé
            if (currentAnimation != previousAnimation)
            {
                currentFrame = 0;
                previousAnimation = currentAnimation;
            }

            // Jouer l'animation appropriée
            
            if (currentAnimation != null && currentAnimation.Length > 0)
            {
                PlayFrameAnimation(deltaTime);
            }
        }

        private void PlayFrameAnimation(float deltaTime)
        {
            animationTimer += deltaTime;
            if (animationTimer >= animationSpeed)
            {
                animationTimer = 0f;
                if (currentAnimation == PlayerDead)
                {
                    // Pour l'animation de mort, on s'arrête à la dernière frame
                    if (currentFrame < currentAnimation.Length - 1)
                    {
                        currentFrame++;
                    }
                }
                else
                {
                    // Pour les autres animations, on boucle
                    
                    currentFrame = (currentFrame + 1) % currentAnimation.Length;
                }
            }
            playerSpriteRenderer.sprite = currentAnimation[currentFrame];
        }
}
