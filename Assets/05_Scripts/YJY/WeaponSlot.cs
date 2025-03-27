using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//각 무기를 슬롯으로 표시-UI제작필요
//무기의 이름과 공격력 정보 표시.드래그앤드롭으로 무기선택
//무기선택 이벤트 처리
public class WeaponSlot : MonoBehaviour
{
    public Text weaponNameText;
    public Text attackPowerText;
    public Button slotButton;

    private Weapon weapon;
    public event System.Action<Weapon> OnWeaponSelected;

    public void SetWeapon(Weapon newWeapon)//무기를 슬롯에 설정
    {

    }
}
