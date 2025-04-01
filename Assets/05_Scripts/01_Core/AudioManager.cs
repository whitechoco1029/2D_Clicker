using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioClip clickClip;
    public AudioClip upgradeClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickClip);
    }

    public void PlayUpgrade()
    {
        sfxSource.PlayOneShot(upgradeClip);
    }
}
