using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audSource;

    public AudioClip winSfx, loseSfx;
    public List<AudioClip> brickSfx;
    public AudioClip dropSfx;
    
    public override void Awake()
    {
        base.Awake();
        audSource = transform.GetComponent<AudioSource>();
    }

    public void PlayLoseSound()
    {
        audSource.PlayOneShot(loseSfx);
    }
    public void PlayWinSound()
    {
        audSource.PlayOneShot(winSfx);
    }
    public void PlayBrickSound()
    {
        audSource.PlayOneShot(brickSfx[Random.Range(0,brickSfx.Count)]);
    }

    public void PlayDropSound()
    {
        //audSource.PlayOneShot(dropSfx,Random.Range(0.5f,1f));
    }

    public void PlayASound(AudioClip i_Sound, float i_Volume)
    {
        audSource.PlayOneShot(i_Sound, i_Volume);
    }
}
