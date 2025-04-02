using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer")]
    public AudioMixer audioMixer;

    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip mainBGM;
    public AudioClip gameBGM;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip clickClip;
    public AudioClip attackClip;
    public AudioClip weaponUpgradeClip;
    public AudioClip enemyDieClip;
    public AudioClip upgradeClip;
    public AudioClip errorClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBGM(mainBGM);
    }

    // BGM 재생
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // SFX 재생
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // 볼륨은 0.0001 ~ 1.0 → dB로 환산 필요
    public void SetBGMVolume(float volume)
    {
        Debug.Log($"Set BGM Volume: {volume}");
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    // 빠르게 자주 쓰는 프리셋용
    public void PlayClick() => PlaySFX(clickClip);
    public void PlayAttack() => PlaySFX(attackClip);
    public void PlayUpgrade() => PlaySFX(upgradeClip);
    public void PlayError() => PlaySFX(errorClip);
    public void PlayWeaponUpgrade() => PlaySFX(weaponUpgradeClip);
    public void PlayEnemyDie() => PlaySFX(enemyDieClip);
}
