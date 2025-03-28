using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private ClickAttack clickAttack;
    private AutoAttack autoAttack;
    public EnemySpawner enemy;

    private void Start()
    {
        clickAttack = GetComponent<ClickAttack>();
        autoAttack = GetComponent<AutoAttack>();
    }

    public void ExecuteClickAttack()
    {
        if (clickAttack != null)
        {
            clickAttack.PerformClickAttack();
        }
    }

    public void SetEnemyTarget(Enemy enemy)
    {
        if (autoAttack != null)
        {
            autoAttack.SetTarget(enemy);
        }
    }
}
