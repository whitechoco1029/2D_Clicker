using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int WeaponID; //무기 번호1,2,3...
    public string waeponName;//이름
    public int WeaponPower;//공격력

    public Weapon(int id, string name, int power)
    {
        WeaponID = id;
        waeponName = name;
        WeaponPower = power;
    }

    public void DisplayWeaponInfo()//무기능력치 출력
    {
        Debug.Log($"번호 :{WeaponID},이름:{waeponName},공격력:{WeaponPower}");
    }
}
