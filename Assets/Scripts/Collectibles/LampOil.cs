using System;
using UnityEngine;

public class LampOil : MonoBehaviour, ICollectible
{
   public float lightIncrease = 10;
   private Lantern _lantern;

   private void Start()
   {
      _lantern = GameObject.FindWithTag("Player").GetComponent<Lantern>();
   }

   public void Collect()
   {
      _lantern.AddLight(lightIncrease);
      Destroy(gameObject);
   }
}
