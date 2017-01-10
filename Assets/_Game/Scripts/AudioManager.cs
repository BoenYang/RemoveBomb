using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public static AudioManager ins;

    public List<AudioClip> Backgrounds; 

    public List<AudioClip> CatchStar;

    public AudioClip ButtonClick; 

    private AudioSource bgAudio;

    private AudioSource effectAudio;

    public bool IsBgAudioPlaying
    {
        get { return bgAudio.isPlaying; }
    }

    private void Awake()
    {
        ins = this;
        AudioSource[] components = GetComponents<AudioSource>();
        bgAudio = components[0];
        effectAudio = components[1];
    }

    public void PlayBackground(int index,bool loop)
    {
        bgAudio.loop = loop;
        bgAudio.clip = Backgrounds[index];
        bgAudio.Play();
    }

    public void PlayCatchEffect(int index)
    {
        effectAudio.PlayOneShot(CatchStar[index]);
    }

    public void PlayButtonClick()
    {
        effectAudio.PlayOneShot(ButtonClick);
    }

    public void StopBackground()
    {
        bgAudio.Stop();
    }

    public void MuteAllAudio(bool mute)
    {
        if (mute)
        {
            bgAudio.Pause();
        }
        else
        {
            bgAudio.Play();
        }

        bgAudio.mute = mute;
        effectAudio.mute = mute;
    }

}
