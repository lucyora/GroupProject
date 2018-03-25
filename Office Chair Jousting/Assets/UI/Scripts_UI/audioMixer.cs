using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioMixer : MonoBehaviour {

    public AudioMixer masterMixer;

    public void setFxVolume(float FxVolume)
    {
        FxVolume = FxVolume * 20;
        masterMixer.SetFloat("EffectsParam", FxVolume);
    }
    public void setMusicVolume(float musicVolume)
    {
        musicVolume = musicVolume * 20;
        masterMixer.SetFloat("MusicParam", musicVolume);
    }
    public void setMasterVolume(float MasterVolume)
    {
        MasterVolume = MasterVolume *20;
        masterMixer.SetFloat("MasterParam", MasterVolume);
    }
}
