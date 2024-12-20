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
    [SerializeField] private AudioClip coinPickupClip;
    [SerializeField] private AudioClip playerHitClip;
    [SerializeField] private AudioClip expPickupClip;
    [SerializeField] private AudioClip levelupClip;


    private void Awake()
    {
        SetAudioSource();
        SetAudioClip();

    }
    private void Start()
    {
        _bgmSource.volume = 0.05f;
        _sfxSource.volume = 0.3f;
    }

    public override void Init()
    {
        base.Init();
        AudioManager audioManager = AudioManager.Instance;
        PlayStartBGM();
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
        bgmClip = Resources.Load<AudioClip>("Audios/BGM_Clip");
        sfxClip = Resources.Load<AudioClip>("Audios/SFX_ButtonClick");
        coinPickupClip = Resources.Load<AudioClip>("Audios/SFX_CoinPickup");
        playerHitClip = Resources.Load<AudioClip>("Audios/SFX_PlayerHit");
        expPickupClip = Resources.Load<AudioClip>("Audios/SFX_ExpPickup");
        levelupClip = Resources.Load<AudioClip>("Audios/SFX_Levelup");
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
    public void PlayCoinPickupSFX() => PlaySFX(coinPickupClip);
    public void PlayExpPickupSFX() => PlaySFX(expPickupClip);
    public void PlayLevelupSFX() => PlaySFX(levelupClip);
    public void PlayPlayerHitSFX()
    {
        PlaySFX(playerHitClip);
        //플레이어 히트판정부분에 AudioManager.Instance.PlayPlayerHitSFX();
    }

   

   

    public float GetBGMVolume() => _bgmSource.volume;
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;

    public float GetSFXVolume() => _sfxSource.volume;
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}
