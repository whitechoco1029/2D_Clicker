using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int weaponID; //무기 번호1,2,3...
    public string weaponName;//이름
    public int WeaponPower;//공격력

    public Weapon(int id)
    {
        weaponID = id;
        weaponName = $"{id}번 무기";
        WeaponPower = id * 10;
    }

    public void DisplayWeaponInfo()//무기능력치 출력
    {
        Debug.Log($"번호 :{weaponID},이름:{weaponName},공격력:{WeaponPower}");
    }
}
