using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAttack : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 마우스 클릭 시
        {
            AttackManager.Instance.ClickAttack();  // AttackManager의 ClickAttack 호출
        }
    }
}