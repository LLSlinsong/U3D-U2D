using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    [SerializeField] private AudioSource[] musicList;
    [SerializeField] private AudioSource[] sfxList;
    [SerializeField] private int levelMusicToPlay = 0;

    [SerializeField] private AudioMixerGroup musicMixer;

    [SerializeField] private AudioMixerGroup sfxMixer;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(levelMusicToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMusic(int musicToPlay)
    {
        foreach(var music in musicList)
        {
            music.Stop();
        }
        musicList[musicToPlay].Play();
    }
    public void PlaySFX(int sfxToPlay)
    {
        sfxList[sfxToPlay].Play();
    }

    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicLevel", UIController.Instance.musixSlider.value);
    }

    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SFXLevel", UIController.Instance.sfxSlider.value);
    }
}
