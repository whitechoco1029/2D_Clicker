using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�� ���⸦ �������� ǥ��-UI�����ʿ�
//������ �̸��� ���ݷ� ���� ǥ��.�巡�׾ص������ ���⼱��
//���⼱�� �̺�Ʈ ó��
public class WeaponSlot : MonoBehaviour
{
    public Text weaponNameText;
    public Text attackPowerText;
    public Button slotButton;

    private Weapon weapon;
    public event System.Action<Weapon> OnWeaponSelected;

    public void SetWeapon(Weapon newWeapon)//���⸦ ���Կ� ����
    {

    }
}
