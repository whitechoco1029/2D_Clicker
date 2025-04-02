using UnityEngine;
using System.IO;
using UnityEngine.Audio;

public class SaveLoadManager : MonoBehaviour
{
    private string savePath;
    private const string BGM_KEY = "BGMVolume";
    private const string SFX_KEY = "SFXVolume";

    public AudioMixer audioMixer; // 연결 필요

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        // 게임 시작 시 자동 로드
        LoadGame();
        LoadVolumeSettings();
    }

    #region 🔹 게임 저장/불러오기

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(GameManager.Instance.userData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("게임 저장 완료");
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            JsonUtility.FromJsonOverwrite(json, GameManager.Instance.userData);
            Debug.Log("게임 불러오기 완료");
        }
        else
        {
            Debug.Log("저장된 게임 데이터가 없습니다.");
        }
    }

    #endregion

    #region 🔊 볼륨 저장/불러오기

    public void SaveVolumeSettings(float bgm, float sfx)
    {
        PlayerPrefs.SetFloat(BGM_KEY, bgm);
        PlayerPrefs.SetFloat(SFX_KEY, sfx);
        PlayerPrefs.Save();
        Debug.Log($"볼륨 저장됨: BGM={bgm}, SFX={sfx}");
    }

    public void LoadVolumeSettings()
    {
        float bgm = PlayerPrefs.GetFloat(BGM_KEY, 1f);
        float sfx = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        SetBGMVolume(bgm);
        SetSFXVolume(sfx);
        Debug.Log($"볼륨 불러오기: BGM={bgm}, SFX={sfx}");
    }

    private void SetBGMVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("BGMVolume", dB);
    }

    private void SetSFXVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("SFXVolume", dB);
    }

    #endregion
}
