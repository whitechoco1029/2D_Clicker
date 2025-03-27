using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int weaponID; //���� ��ȣ1,2,3...
    public string weaponName;//�̸�
    public int WeaponPower;//���ݷ�

    public Weapon(int id)
    {
        weaponID = id;
        weaponName = $"{id}�� ����";
        WeaponPower = id * 10;
    }

    public void DisplayWeaponInfo()//����ɷ�ġ ���
    {
        Debug.Log($"��ȣ :{weaponID},�̸�:{weaponName},���ݷ�:{WeaponPower}");
    }
}
