using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    private UserData userData;
    public float attackInterval = 1f; // ���� �ֱ�
    public EnemySpawner enemy;

    public void Start()
    {
        userData = FindObjectOfType<UserData>();
        StartCoroutine(AutoAttackRoutine());
    }

    public void SetTarget(Enemy enemy)
    {
        targetEnemy = enemy;
    }

    public IEnumerator AutoAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            if (targetEnemy == null)
            {
                targetEnemy = FindObjectOfType<Enemy>(); // �ڵ����� ���� ã��
            }
            if (targetEnemy != null)
            {
                targetEnemy.TakeDamage(userData.clickDmg);
            }
        }
    }
}