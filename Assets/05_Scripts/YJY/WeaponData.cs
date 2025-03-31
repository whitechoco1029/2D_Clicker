using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDataConsumable
{
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class WeaponData : ScriptableObject
{
    public string displayName;
    public int weaponID;
    public Sprite icon;
    public int weaponPower;
}
