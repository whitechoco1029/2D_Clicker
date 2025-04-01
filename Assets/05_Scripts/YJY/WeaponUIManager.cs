using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Xml.Linq;

//�κ��丮 �ý����� �־�� ���� �������� �����ϰ� ������ �����ҵ���
//����������� �巡���Ͽ� �ռ��� �� �ִ� ��� �߰��ؾߵ�
public class WeaponUIManager : MonoBehaviour
{
    public Button generateWeaponButton; //������� ��ư

    public List<Slot> slots = new List <Slot>();
    public WeaponData currentdata; //������ �ҷ����� -1���� ����...
    public Slot firstWeapon = null; //ù��° ���õ� ����
    public Slot secondWeapon = null; //�ι��� ���õ� ����

    private List<WeaponData> inventory = new List<WeaponData>();//���� ���
 

    void Start()
    {
       
        generateWeaponButton.onClick.AddListener(spawnWeapon);

        // �� ���Կ� ���� Slot ������Ʈ�� �Ҵ�
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            slots.Add (transform.GetChild(0).GetChild(i).GetComponent<Slot>());  // �� ���Կ� ItemSlot ������Ʈ ����
        }
    }

    // ���� �ռ� ó��
    public void UpgradeWeapons()
    {
        if (firstWeapon != null && secondWeapon != null)
        {
            if (firstWeapon.weapondata.weaponID == secondWeapon.weapondata.weaponID)  // ������ �������� Ȯ��
            {
                int newWeaponID = firstWeapon.weapondata.weaponID + 1;

                // ���ο� ���� ����
                WeaponData upgradedWeapon = GetWeaponData(newWeaponID);

                // ���õ� ���� �ʱ�ȭ              
                firstWeapon.SetWeapon(null);
                secondWeapon.SetWeapon(upgradedWeapon);
            }
        }
    }
 
    public void spawnWeapon()
    {

        Slot select = GetEmptySlot();
        if (select != null)
        {
            
            select.SetWeapon(currentdata);
        }

    }
    private Slot GetEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].weapondata == null)
            {
                return slots[i];
            }

        }
        return null; //��ĭ�� ������                        
    }
    private WeaponData GetWeaponData(int ID)
    {
        for (int i = 0; i < inventory.Count; i++)

        {
            if (inventory[i].weaponID == ID)
            {
                return inventory[i];
            }
        }
        return null;
    }
}