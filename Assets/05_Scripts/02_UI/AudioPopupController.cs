using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioPopupController : MonoBehaviour
{
    [Header("�˾�â")]
    public GameObject popupPanel;

    [Header("���� �����̴�")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("����� �ͼ�")]
    public AudioMixer audioMixer;

    private void Start()
    {
        popupPanel.SetActive(false); // ���� �� ��������

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
