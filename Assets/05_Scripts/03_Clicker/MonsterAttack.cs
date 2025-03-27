using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour
{
    public float attackInterval = 2.0f; // ���� ���� ����
    public int attackDamage = 10; // ������ ���ݷ�
    private GameObject player; // �÷��̾� ���

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(AutoAttackRoutine());
    }

    IEnumerator AutoAttackRoutine()
    {
        while (player != null)
        {
            yield return new WaitForSeconds(attackInterval);

            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        Debug.Log("���Ͱ� �÷��̾ ����! ������: " + attackDamage);
        // ���⿡ �÷��̾� ü�� ���� ���� �߰� ����
    }
}