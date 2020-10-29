using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    /*
     * Makes footstep sounds when the player is walking.
     */

    CharacterController cc;
    AudioSource audio;
    public float minVol;
    public float maxVol;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(cc.velocity.magnitude > 0 && !audio.isPlaying && cc.isGrounded) 
        {
            audio.volume = Random.Range(minVol, maxVol);
            audio.pitch = Random.Range(0.8f, 1.1f);

            audio.Play();
        }
    }
}
