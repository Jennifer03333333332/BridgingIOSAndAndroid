using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public struct SoundGroup
    {
        public AudioSource audioSource;
        public string soundName;
    }
    //[SerializeField]
    public List<SoundGroup> sound_List = new List<SoundGroup>();

    //public AudioSource IntroBGM;
    //public AudioSource OneToTwoBGM;
    //public AudioSource TwoToThreeBGM;
    //public AudioSource ConclusionBGM;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Playing sound on where player is.
    public void PlayingSound(string _soundName)
    {
        //AudioSource.PlayClipAtPoint(sound_List[FindSound(_soundName)].audioClip, Camera.main.transform.position);
        sound_List[FindSound(_soundName)].audioSource.Play();

    }
    public void PlayingSound(int sound_index)
    {
        sound_List[sound_index].audioSource.Play();

    }
    public void StopPlayingSound(string _soundName)
    {
        sound_List[FindSound(_soundName)].audioSource.Stop();
    }

    public void StopAllPlayingSound()
    {
        foreach (var obj in sound_List)
        {
            obj.audioSource.Stop();
        }
    }

    //For each audio
    public void SetIntroVolume(float v)
    {
        sound_List[0].audioSource.volume = v;
    }
    public void SetOneToTwoVolume(float v)
    {
        sound_List[1].audioSource.volume = v;
    }
    public void SetTwoToThreeVolume(float v)
    {
        sound_List[2].audioSource.volume = v;
    }
    public void SetConclusionVolume(float v)
    {
        sound_List[3].audioSource.volume = v;
    }

    public void MuteVolume(int index)
    {
        bool state = sound_List[index].audioSource.mute;
        sound_List[index].audioSource.mute = !state;
    }





    public int FindSound(string _soundName)
    {
        int i = 0;
        while (i < sound_List.Count)
        {
            if (sound_List[i].soundName == _soundName)
            {
                return i;
            }
            i++;
        }
        return i;
    }
}
