using UnityEngine;
using UnityEngine.EventSystems;

public class AutoAttack : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating(nameof(PerformAutoAttack),
                        AttackManager.Instance.autoAttackInterval,
                        AttackManager.Instance.autoAttackInterval);
    }

    private void PerformAutoAttack()
    {
        AttackManager.Instance.AutoAttack();
    }
}
