using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
   

   Slider slider;

   void Awake()
   {
      // slider = GetComponent<Slider>();
   }

   void Start()
   {
       slider.maxValue = Gamemanager.instance.GetPlayer.Health;
       slider.value = Gamemanager.instance.GetPlayer.Health;
   }
   public void UpdateHealth() => slider.value = Gamemanager.instance.GetPlayer.Health;
}