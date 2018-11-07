using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public AudioClip hit4;
    public AudioClip hit5;
    public AudioClip hit6;
    public AudioClip hit7;
    public AudioClip hit8;
    public AudioClip hit9;
    public AudioClip hit10;
    public AudioClip hit11;
    public AudioClip hit12;
    public AudioClip hit13;
    public AudioClip hit14;
    //public AudioClip BGM;
    public AudioSource audio_source;
    public AudioSource bgm_source;
    public AudioClip throw1;
    public AudioClip throw2;
    public AudioClip throw3;

    private AudioClip[] hitsounds;
    private AudioClip[] throwsounds;

    // Use this for initialization
    void Start () {
        hitsounds = new AudioClip[] {hit1, hit2, hit3, hit4, hit5, hit6, hit7, hit8, hit9, hit10, hit11, hit12, hit13, hit14};
        throwsounds = new AudioClip[] { throw1, throw2, throw3 };
        bgm_source.Play();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void PlayHitSound(int i)
    {

        audio_source.PlayOneShot(hitsounds[i], 0.8f);
    }
    public void PlayThrowSound(int i)
    {
        audio_source.PlayOneShot(throwsounds[i], 0.8f);
    }
}
