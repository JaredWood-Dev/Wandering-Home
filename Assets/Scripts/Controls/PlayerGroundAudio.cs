using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerGroundAudio : MonoBehaviour
{
    /*
     * This script connects Ground Materials to Audio
     * So walking on a Wooden Material Sounds like Wood
     */

    
    public List<MaterialAudio> audioMaterials;

    private Rigidbody2D _rb;
    private PlayerController _pc;
    private AudioSource _as;
    public ContactFilter2D groundLayers;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pc = GetComponent<PlayerController>();
        _as = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_rb.linearVelocity.magnitude > 0 && _pc.isGrounded)
        {
            foreach (var audioMaterial in audioMaterials)
            {
                if (_rb.GetContactColliders(groundLayers).Length > 0)
                {
                    if (audioMaterial.material == _rb.GetContactColliders(groundLayers)[0].sharedMaterial)
                    {
                        AudioClip sound = audioMaterial.audioClips[new Random().Next(audioMaterial.audioClips.Length)];
                        if (!_as.isPlaying)
                        {
                            _as.PlayOneShot(sound);
                        }
                    }
                }
            }
        }
        else
        {
            _as.Stop();
        }
    }
}

[System.Serializable]
public class MaterialAudio
{
    public PhysicsMaterial2D material;
    public AudioClip[] audioClips;
}
