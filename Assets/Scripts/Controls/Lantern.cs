using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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

    [Header("Light Component")] 
    public float maxRadius = 5;

    private SpriteRenderer _s;
    private Light2D _l;


    void Start()
    {
        _s = GetComponent<SpriteRenderer>();
        _l = lanternLight.transform.GetChild(0).GetComponent<Light2D>();
        currentLight = maxLight;
        _l.pointLightOuterRadius = maxRadius;
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

        currentLight -= Time.deltaTime * drainSpeed;
        _l.pointLightOuterRadius = maxRadius * (currentLight / maxLight);

        if (currentLight <= 0)
        {
            print("light ran out");
            Destroy(this);
            SceneManager.LoadScene(0);
        }
    }

    public void AddLight(float amount)
    {
        currentLight = Mathf.Min(currentLight + amount, maxLight);
    }
}
