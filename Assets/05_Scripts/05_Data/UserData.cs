using System;
using UnityEngine;

[Serializable]
public class UserData
{
    // 진행 상황
    public int stage = 1;
    public float gold = 0;

    // 기본 스탯
    public float atk = 1f;
    public float critHit = 0f;
    public float critDmg = 1.5f;
    public float atkSpeed = 1f;
    public float goldBonus = 0f;
    public float clickDmg = 1f;

    // 업그레이드 및 장비
    public UpgradeData upgradeData = new UpgradeData();
    public EquipmentData equipmentData = new EquipmentData();
}

[Serializable]
public class UpgradeData
{
    public int atkLevel = 1;
    public int critHitLevel = 0;
    public int critDmgLevel = 0;
    public int atkSpeedLevel = 0;
    public int goldBonusLevel = 0;
    public int clickDmgLevel = 0;
}

[Serializable]
public class EquipmentData
{
    public string equippedWeapon = "";
    public string equippedAccessory = "";
}

public static class SaveSystem
{
    private static string saveKey = "UserData";

    public static void Save(UserData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();
    }

    public static UserData Load()
    {
        if (!PlayerPrefs.HasKey(saveKey)) return new UserData();
        string json = PlayerPrefs.GetString(saveKey);
        return JsonUtility.FromJson<UserData>(json);
    }
}

