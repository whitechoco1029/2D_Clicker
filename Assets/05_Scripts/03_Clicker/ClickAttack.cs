using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAttack : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))  // ���콺 Ŭ�� ��
        {
            AttackManager.Instance.ClickAttack();  // AttackManager�� ClickAttack ȣ��
        }
    }
}