using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource efxSource;
    public AudioSource selectSound;
	public AudioSource confirmSound;
	public AudioSource deselectSound;
	public AudioSource readySound;
	public AudioSource transitionSound;
	public static SoundManager instance = null;

	public float sfxVol;
	public float musicVol;

	//TESTING PITCHES
	public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}

	/* TESTING 
    public void PlaySingle (AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
	*/
}
