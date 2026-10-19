using System;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [NonSerialized]
    public Lantern _l;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_l)
            {
                _l.isSafe = true;
                _l.AddLight(1000);
            }
            else
            {
                if (other.gameObject.GetComponent<Lantern>())
                {
                    _l = other.gameObject.GetComponent<Lantern>();
                    _l.isSafe = true;
                    _l.AddLight(1000);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_l)
            {
                _l.isSafe = false;
            }
            else
            {
                if (other.gameObject.GetComponent<Lantern>())
                {
                    _l = other.gameObject.GetComponent<Lantern>();
                    _l.isSafe = false;
                }
            }
        }
    }
}
