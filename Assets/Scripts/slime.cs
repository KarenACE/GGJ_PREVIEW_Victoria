using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator anim;
    SpriteRenderer spr; 
    [SerializeField,Range(0.1f,10f)]
    float moveSpeed = 3f;
    [SerializeField]
    Vector2 direction = Vector2.right;
    float timer = 0f;
     [SerializeField,Range(0.1f,10f)]
    float patrolTimelimit = 3f;
     [SerializeField,Range(0.1f,10f)]

    float idlingTime = 2f;

    IEnumerator patroling;

     IEnumerator idling;

     IEnumerator attack; 

     IEnumerator lastRoutine; 

     [SerializeField,Range(0.1f,5f)]
     float rayDistance = 2f;

     [SerializeField]
     Color rayColor = Color.red;

     [SerializeField]

    LayerMask playerLayer;
    [SerializeField]
     LayerMask groundLayer;
     [SerializeField]

      Vector3 rayOrigin;
      [SerializeField]
      
      AnimationClip attackClip; 
      [SerializeField]
      float attackClipOffset = 1f;
      bool isAttacking = false;
      [SerializeField]
      int damage  = 2;


    
    void Awake()
    {
  rb2D =GetComponent<Rigidbody2D>();
  anim =GetComponent<Animator>();
  spr=GetComponent<SpriteRenderer>();
    }
    void Start()
    {
       
        SartPatroling();
        IEnumerator lastRoutine; 
    }
    IEnumerator PatrolingRoutine()
    {
        anim.SetFloat("patrol",1f);
        while(true)
        { 
            if(Attack && !isAttacking)
            {
                isAttacking = true;
                lastRoutine = PatrolingRoutine();
                StartAttack();
               yield break;
            }
        rb2D.position += direction * moveSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        if(timer >= patrolTimelimit)
        
        {      
            StartIdling();
            yield break;
         }
         yield return null;
        }
        
    }
IEnumerator IdlingRoutine()
{
    anim.SetFloat("patrol",0f);
    yield return new WaitForSeconds(idlingTime);
    SartPatroling();
} 
IEnumerator AttackingRoutine ()
{
    anim.SetTrigger("attack");
     yield return new WaitForSeconds(attackClip.length + attackClipOffset);
    StartCoroutine(lastRoutine);
    isAttacking = false;
}
    void StartAttack()
    {
        attack = AttackingRoutine();
        StartCoroutine(attack);
    }
     void StartIdling()
     { 
        idling = IdlingRoutine();
        StartCoroutine(idling);
     }

     void SartPatroling()
     {     
          
          timer = 0f;
          direction = direction == Vector2.right ? Vector2.left : Vector2.right;
          spr.flipX = FlipSpriteX; 
          patroling = PatrolingRoutine();
          StartCoroutine(patroling);

     }
  
  void StarrIA()
  {
       patroling = PatrolingRoutine();
          StartCoroutine(patroling);
  }
    
    void Update()
    {
        if(Attack)
        {
            Debug.Log("attack"); 
        }
    }
    bool FlipSpriteX => direction == Vector2.right ? false : true;

    bool Attack => Physics2D.Raycast(transform.position + rayOrigin,Vector2.right,rayDistance,playerLayer) ||
     Physics2D.Raycast(transform.position + rayOrigin,Vector2.left,rayDistance,playerLayer); 

    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + rayOrigin,Vector3.right * rayDistance); 
        Gizmos.DrawRay(transform.position + rayOrigin,Vector3.left * rayDistance); 
    }

    public int GetDamage => damage;
     
    public void MakeDamage()
    {

        Gamemanager.instance.GetPlayer.Health -= damage;
        Gamemanager.instance.GetHealthBar.UpdateHealth();
    }
  
    

} 
  
