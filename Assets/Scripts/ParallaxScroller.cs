using System;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    private float _startPos, _length;
    public GameObject cam;
    [Tooltip("A value of 0 moves exactly with the camera, 0.5 lags behind the camera, 1 makes it static.")]
    [Range(0.0f, 1.0f)]
    public float parallaxEffect = 0.5f;

    private void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        
        cam = Camera.main.gameObject;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

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
