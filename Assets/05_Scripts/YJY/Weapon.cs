using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//각 무기 기본정보, 합성 후 새 무기 생성기능

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
}
