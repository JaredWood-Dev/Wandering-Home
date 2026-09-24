using System;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    /*
    This script handles Coal's lantern. It will gradually run out of fuel, and move with Coal.
    */

    public float drainSpeed;
    public float maxLight;
    public float currentLight;
    public GameObject lanternLight;
    public Vector2 lanternOffset;
    public float lanternMoveSpeed;

    private SpriteRenderer _s;


    void Start()
    {
        _s = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        float dir = 1;
        if (_s.flipX)
        {
            dir = -1;
        }
        
        lanternLight.transform.position = Vector2.Lerp(
            lanternLight.transform.position,
            (Vector2)transform.position + new Vector2(lanternOffset.x * dir, lanternOffset.y),
            Time.deltaTime * lanternMoveSpeed);
    }
}
