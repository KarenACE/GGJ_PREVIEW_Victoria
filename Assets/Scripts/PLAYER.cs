using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{  
    //El Serielize...es para que salga en unity, el speed es para movimientos de lado y el jump pa brincar.
    [SerializeField]
    float speed = 3.0f;
     [SerializeField]
    float jumpForce = 7.0f; //Pa Saltar
    Rigidbody2D rb2D; //Pa saltar
    SpriteRenderer spr; //Para hacer Flip
    Animator anim; //Para  controlar el animator
    [SerializeField,Range(0.01f,10f)]
    float rayDistance = 2f;
    [SerializeField]
    Color rayColor = Color.red;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Vector3 rayOrigin;
[   SerializeField]
    Score score;

   GameInputs gameInputs;

   [SerializeField]

   int health; 

   void Awake ()
{
    gameInputs = new GameInputs();

}

void OnEnable()
{    
    gameInputs.Enable();
    //gameInputs.Disable();
}
void OnDisable()
{
    gameInputs.Disable();
}
    void Start () //Pa saltar
{
    rb2D = GetComponent<Rigidbody2D>();    //Pa saltar.
    spr = GetComponent<SpriteRenderer>();  //Para hacer FLIP
    anim = GetComponent<Animator>();
    gameInputs.Gameplay.Jump.performed +=  _=> Jump();
    gameInputs.Gameplay.Jump.canceled +=  _=> JumpCanceled();

}
//Es el update pero para cosas de fisica, porque se ejecuta n veces durante cada frame.
void FixedUpdate()
{
  rb2D.position += Vector2.right * Axis.x * speed * Time.fixedDeltaTime;
}

    void Update()

    {
    
        spr.flipX = FlipSprite;
    }

    void Jump ()
    {
         if(IsGrounding)
       { 
           rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
           anim.SetTrigger("jump");
    
    }
    }
    void JumpCanceled()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x,0f);

    }
  void LateUpdate()
     {
         anim.SetFloat("AxisX",Mathf.Abs(Axis.x));
         anim.SetBool("ground",IsGrounding);

     }

//depreciated
   Vector2 Axis => new Vector2(gameInputs.Gameplay.AxisX.ReadValue<float>(),gameInputs.Gameplay.AxisY.ReadValue<float>());
   //Vector2 AxisRaw => new Vector2 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
  //Input.GetAxisRaw es para que se detenga en seco, no suave.
  //******
  //  bool JumpButton => Input.GetButtonDown("Jump"); //Pa saltar.
  bool FlipSprite => Axis.x > 0 ? false : Axis.x < 0 ? true : spr.flipX;  // Es un If Else que nos hace tener al character viendo a la izq o derecha
  bool IsGrounding => Physics2D.Raycast(transform.position + rayOrigin,Vector2.down,rayDistance,groundLayer);
void OnTriggerEnter2D(Collider2D col)
 {
    if(col.CompareTag("coin"))
    {
       Coin coin = col.GetComponent<Coin>();
       score.AddPoints(coin.GetPoints); 
       Destroy(col.gameObject);
       
   }

 }
 void OnDrawGizmosSelected()
 {
     Gizmos.color = rayColor;
     Gizmos.DrawRay(transform.position + rayOrigin,Vector2.down* rayDistance);
 }
 public int Health {get => health; set => health = health > 0 ? value:0;}
}





 

