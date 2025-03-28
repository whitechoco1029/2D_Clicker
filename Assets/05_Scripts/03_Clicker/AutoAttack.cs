using System.Collections;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public  void Start()
    {
        StartCoroutine(AutoAttackRoutine());  // �ڷ�ƾ ����
    }

    public IEnumerator AutoAttackRoutine()
    {
        while (true)  // ���� ���� (���ǿ� ���� ���� ����)
        {
            if (AttackManager.Instance != null)
            {
                AttackManager.Instance.AutoAttack();  // ���� ����
            }

            yield return new WaitForSeconds(1f);  // 1�ʸ��� ����
        }
    }
}