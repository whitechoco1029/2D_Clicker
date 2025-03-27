using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int WeaponID; //���� ��ȣ1,2,3...
    public string waeponName;//�̸�
    public int WeaponPower;//���ݷ�

    public Weapon(int id, string name, int power)
    {
        WeaponID = id;
        waeponName = name;
        WeaponPower = power;
    }

    public void DisplayWeaponInfo()//����ɷ�ġ ���
    {
        Debug.Log($"��ȣ :{WeaponID},�̸�:{waeponName},���ݷ�:{WeaponPower}");
    }
}
