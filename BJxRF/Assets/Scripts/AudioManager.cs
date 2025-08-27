using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        sfxSource.volume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Som não encontrado");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void Func(string name1, string name2)
    {
        Sound s1 = Array.Find(musicSounds, x => x.soundName == name1);
        Sound s2 = Array.Find(musicSounds, x => x.soundName == name2);

        if (s1 == null || s2 == null)
        {
            Debug.Log("Algum som não encontrado");
        }
        else
        {
            StartCoroutine(IESoundChange(s1, s2));
        }
    }

    IEnumerator IESoundChange(Sound s1, Sound s2)
    {
        // Toca a primeira música
        musicSource.clip = s1.clip;
        musicSource.Play();
        double startTime = AudioSettings.dspTime + musicSource.clip.length;
        // Espera a primeira música terminar
        yield return new WaitForSeconds(s1.clip.length - 0.99f);

        // Troca para a segunda música e ativa o loop
        musicSource.clip = s2.clip;
        musicSource.PlayScheduled(startTime);
        musicSource.loop = true;
    }



    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Som não encontrado");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
    }

    public AudioSource GetMusicAudioSource(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Som não encontrado: " + name);
            return null;
        }


        GameObject soundObject = new GameObject("Music_" + name);
        AudioSource newSource = soundObject.AddComponent<AudioSource>();

        newSource.clip = s.clip;
        newSource.volume = musicSource.volume;
        newSource.loop = false;
        newSource.playOnAwake = false;

        Destroy(soundObject, s.clip.length + 0.1f);

        return newSource;
    }

}
