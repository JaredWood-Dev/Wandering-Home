using System;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    private float _startPos, _length;
    public GameObject cam;
    [Tooltip("A value of 0 moves exactly with the camera, 0.5 lags behind the camera, 1 makes it static.")]
    [Range(0.0f, 1.0f)]
    public float parallaxEffect = 0.5f;
    public bool isVertical = false;

    private void Start()
    {
        if (isVertical)
        {
            _startPos = transform.position.y;
            _length = GetComponent<SpriteRenderer>().bounds.size.y;
        }
        else
        {
            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }
        
        
        cam = Camera.main.gameObject;
    }

    private void FixedUpdate()
    {
        float distance;
        float movement;
        if (isVertical)
        {
            distance = cam.transform.position.y * parallaxEffect;
            movement = cam.transform.position.y * (1 - parallaxEffect);
            
            transform.position = new Vector3(cam.transform.position.x, _startPos + distance, transform.position.z);
        }
        else
        {
            distance = cam.transform.position.x * parallaxEffect;
            movement = cam.transform.position.x * (1 - parallaxEffect);
            
            transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);
        }
        

        if (movement > _startPos + _length)
        {
            _startPos += _length;
        }
        else if (movement < _startPos - _length)
        {
            _startPos -= _length;
        }
    }
}
