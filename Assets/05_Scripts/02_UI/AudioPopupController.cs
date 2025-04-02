using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioPopupController : MonoBehaviour
{
    [Header("팝업창")]
    public GameObject popupPanel;

    [Header("볼륨 슬라이더")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("오디오 믹서")]
    public AudioMixer audioMixer;

    private void Start()
    {
        popupPanel.SetActive(false); // 시작 시 꺼져있음

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void OpenPopup()
    {
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    private void SetBGMVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("BGMVolume", dB);
    }

    private void SetSFXVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("SFXVolume", dB);
    }
}
