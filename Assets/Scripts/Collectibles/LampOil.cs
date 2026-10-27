using System;
using UnityEngine;

public class LampOil : MonoBehaviour, ICollectible
{
   public float lightIncrease = 10;
   private Lantern _lantern;
   private AudioSource _a;

   private void Start()
   {
      _lantern = GameObject.FindWithTag("Player").GetComponent<Lantern>();
      _a = GetComponent<AudioSource>();
   }

   public void Collect()
   {
      _lantern.AddLight(lightIncrease);
      _a.Play();
      GetComponent<SpriteRenderer>().enabled = false;
      Invoke("DestroyObject", _a.clip.length);
   }

   void DestroyObject()
   {
      gameObject.SetActive(false);
   }
}
