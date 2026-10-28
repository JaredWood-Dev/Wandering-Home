# Wandering-Home
_Wandering Home_ is a Unity Technical Demo that serves as a showcase of a gameplay mechanic and a way to expand my skill set and tool box as a video game developer. In this game, you play as a Dragonborn adventurer who is attempting to get home, but in the dark. You have a lantern that slowly runs out of oil. The less oil you have the less light to see with.

## Usage
The game can be played here: https://dragonbrn.itch.io/wandering-home
You use `A` and `D` to more left and right, `Space` to jump, and `E` interacts with certain objects and people.

## Concepts Learned
In a pursuit to learn new skills, this game touches on a number of new skills.

### Sound Design
This project contains a song that had multiple iterations of development, and sound effects sources from an Open Art archive. A notable feature around the sound effects is the system that allows a developer to link sound effects to specific Physics2D Materials:
```c#
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
    public float minPitch = 0.99f;
    public float maxPitch = 1.01f;
    public float minVolume = 0.85f;
    public float maxVolume = 1f;

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
                            _as.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
                            _as.volume = UnityEngine.Random.Range(minVolume, maxVolume);
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
```

This makes level design very simple and allows the inspector to be used to link sounds to Materials.

### Lighting
A large component of this project was fulling embracing the Lighting2D system in Unity. This allowed me to create various light sources, shadows, and the dynamically changing light source for the lantern. Lighting is a critical component in this game's function and atmosphere. 
