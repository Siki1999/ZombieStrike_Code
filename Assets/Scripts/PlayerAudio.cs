using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip aWalk;
    public AudioClip aRun;
    public AudioClip aJump;
    public AudioClip aWater;
    private CharacterController caracter;
    private float nextWalk = 0.4f;
    private float nextWaterWalk = 0.4f;
    private float nextRun = 0.25f;
    private float nextJump = 0.5f;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        caracter = GetComponent<CharacterController>();
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) 
            && !Input.GetKey("left shift") && !Input.GetKey("space") && caracter.isGrounded && player.localPosition.y >= -1.5f && Time.time >= nextWalk)
        {
            nextWalk = Time.time + 0.4f;
            audio.pitch = Random.Range(0.8f, 1);
            audio.PlayOneShot(aWalk, 0.05f);
        }
        else if ((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) 
            && Input.GetKey("left shift") && !Input.GetKey("space") && caracter.isGrounded && player.localPosition.y >= -1.5f)
        {
            if(Time.time >= nextRun)
            {
                nextRun = Time.time + 0.3f;
                audio.pitch = Random.Range(0.8f, 1);
                audio.PlayOneShot(aRun, 0.05f);
            }
        }
        else if (Input.GetKey("space") && Time.time >= nextJump && player.localPosition.y >= -1.5f)
        {
            nextJump = Time.time + 0.55f;
            audio.PlayOneShot(aJump, 0.4f);
        }
        else if ((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            && !Input.GetKey("space") && caracter.isGrounded && player.localPosition.y < -2.8f && Time.time >= nextWaterWalk)
        {
            nextWaterWalk = Time.time + 0.4f;
            audio.pitch = Random.Range(0.8f, 1);
            audio.PlayOneShot(aWater, 1);
        }
    }
}
