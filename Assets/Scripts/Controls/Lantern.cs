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
    //public Vector2 lanternOffset;
    //public float lanternMoveSpeed;
    [Tooltip("If true, Coal's lantern will be filled up and will not lose light. This is enabled if in a safe enviroment.")]
    public bool isSafe = false;

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

        /*
        lanternLight.transform.position = Vector2.Lerp(
            lanternLight.transform.position,
            (Vector2)transform.position + new Vector2(lanternOffset.x * dir, lanternOffset.y),
            Time.deltaTime * lanternMoveSpeed);
            */

        //If we are in a Safe Zone, do not lose light
        if (!isSafe)
        {
            currentLight -= Time.deltaTime * drainSpeed;
            _l.pointLightOuterRadius = maxRadius * (currentLight / maxLight);
        }

        if (currentLight <= 0)
        {
            print("light ran out");
            AddLight(1000);
            StartCoroutine(GetComponent<ResetPlayer>().TriggerReset());
        }
    }

    public void AddLight(float amount)
    {
        currentLight = Mathf.Min(currentLight + amount, maxLight);
    }
}
