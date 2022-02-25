using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
   public static Gamemanager instance;

   PLAYER player;
   HealthBar healthBar;

   void Awake ()
   {
       if (!instance)
       { 
       instance = this;
       DontDestroyOnLoad(gameObject);
   }
   else 
   {
       Destroy(gameObject);
     }
   }  

   void Start ()
   {
       player = GameObject.FindWithTag("Player").GetComponent<PLAYER>();
       healthBar = GameObject.FindWithTag("Health").GetComponent<HealthBar>(); 
   }
   public PLAYER GetPlayer => player;
   public HealthBar GetHealthBar => healthBar;

  }
  