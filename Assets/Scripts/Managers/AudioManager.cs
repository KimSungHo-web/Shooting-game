using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private GameObject _bgmObj;
    private GameObject _sfxObj;

    private AudioSource _bgmSource;
    private AudioSource _sfxSource;

    [Header("BGM")]
    [SerializeField] private AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioClip sfxClip;

    private void Awake()
    {
        SetAudioSource();
        SetAudioClip();
    }
    private void Start()
    {
        _bgmSource.volume = 0.3f;
        _sfxSource.volume = 0.3f;
    }
    private void SetAudioSource()
    {
        _bgmObj = new GameObject("@BGM");
        _bgmObj.transform.parent = transform;
        _bgmSource = _bgmObj.AddComponent<AudioSource>();

        _sfxObj = new GameObject("@SFX");
        _sfxObj.transform.parent = transform;
        _sfxSource = _sfxObj.AddComponent<AudioSource>();
    }

    private void SetAudioClip()
    {
        //bgmClip = ResourceLoad<AudioClip>("Audios/BGM_Clip");
        //sfxClip = ResourceLoad<AudioClip>("Audios/SFX_ButtonClick");
    }

    public void PlayBGM(AudioClip clip)
    {
        if (_bgmSource.clip != clip)
        {
            _bgmSource.clip = clip;
            _bgmSource.loop = true;
            _bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void PlayStartBGM() => PlayBGM(bgmClip);
    public void PlayClickSFX() => PlaySFX(sfxClip);

    public float GetBGMVolume() => _bgmSource.volume;
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;

    public float GetSFXVolume() => _sfxSource.volume;
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}
