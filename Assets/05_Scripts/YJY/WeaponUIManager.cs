using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

//�κ��丮 �ý����� �־�� ���� �������� �����ϰ� ������ �����ҵ���
//����������� �巡���Ͽ� �ռ��� �� �ִ� ��� �߰��ؾߵ�
public class WeaponUIManager : MonoBehaviour
{
    public GameObject weaponSlotPrefab;   // ���� ���� ������
    public Transform inventoryPanel;      // �κ��丮 �г�
    public List<Weapon> inventory = new List<Weapon>();  // ���� ���
    public Button generateWeaponButton; //���� ���� ��ư

    private Weapon firstWeapon = null;    // ù ��° ���õ� ����
    private Weapon secondWeapon = null;   // �� ��° ���õ� ����

    void Start()
    {
        // ���÷� �κ��丮�� ���� �߰�
        for (int i = 1; i <= 10; i++)
        {
            Weapon weapon = new Weapon(i);
            inventory.Add(weapon);
        }

        // �κ��丮 UI �ʱ�ȭ
        PopulateInventory();

        // ���� ���� ��ư �̺�Ʈ ����
        generateWeaponButton.onClick.AddListener(GenerateWeapon);
    }
    void PopulateInventory() // �κ��丮 UI�� ���� ������ �������� ����
    {
        // ������ UI �׸��� ��� ���� (�ߺ� ����)
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // ���� �����͸� ������� ������ �����Ͽ� UI�� ǥ��
        foreach (Weapon weapon in inventory)
        {
            GameObject weaponSlot = Instantiate(weaponSlotPrefab, inventoryPanel);
            WeaponSlot slot = weaponSlot.GetComponent<WeaponSlot>();

            slot.SetWeapon(weapon);  // ���Կ� ���� ����
            slot.OnWeaponSelected += HandleWeaponSelection; // ���� ���� �̺�Ʈ
        }
    }

    private void HandleWeaponSelection(Weapon selectedWeapon)   // ���� ���� ó��
    {
        if (firstWeapon == null)
        {
            firstWeapon = selectedWeapon;
            Debug.Log($"First Weapon Selected: {firstWeapon.weaponName}");
        }
        else if (secondWeapon == null)
        {
            secondWeapon = selectedWeapon;
            Debug.Log($"Second Weapon Selected: {secondWeapon.weaponName}");

            // �� ���Ⱑ ���õǸ� ��ȭ ����
            UpgradeWeapons();
        }
    }

    // ���� ���� �Լ�
    private void GenerateWeapon()
    {
        // ���� ID�� �κ��丮 ũ�� + 1�� ���� (���ο� ���� ID ����)
        int newWeaponID = inventory.Count + 1;
        Weapon newWeapon = new Weapon(newWeaponID);

        // ������ ���⸦ �κ��丮�� �߰�
        inventory.Add(newWeapon);

        Debug.Log($"Generated New Weapon: {newWeapon.weaponName}");

        // UI ����
        RefreshInventoryUI();
    }

    // ���� ��ȭ �Լ�
    private void UpgradeWeapons()
    {
        if (firstWeapon != null && secondWeapon != null)
        {
            // �� ���⸦ �����Ͽ� ���ο� ���� ���� (ID + 1 ����)
            Weapon upgradedWeapon = new Weapon(firstWeapon.weaponID + 1);
            upgradedWeapon.WeaponPower = (firstWeapon.WeaponPower + secondWeapon.WeaponPower) / 2;

            Debug.Log($"Upgraded to: {upgradedWeapon.weaponName} with Attack Power: {upgradedWeapon.WeaponPower}");

            // ��ȭ�� ���⸦ �κ��丮�� �߰��ϰ�, �� ����� ����
            inventory.Add(upgradedWeapon);
            inventory.Remove(firstWeapon);
            inventory.Remove(secondWeapon);

            // UI ����
            RefreshInventoryUI();

            // ���õ� ���� �ʱ�ȭ
            firstWeapon = null;
            secondWeapon = null;
        }
    }

    // �κ��丮 UI ����
    void RefreshInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);  // ���� ���� ����
        }

        PopulateInventory();  // �ٽ� UI ����
    }
}

