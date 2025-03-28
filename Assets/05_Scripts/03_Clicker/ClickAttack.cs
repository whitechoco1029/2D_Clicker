using UnityEngine;
using System.Collections;

// 클릭 공격을 담당하는 스크립트
public class ClickAttack : MonoBehaviour
{
    public UserData userData;
    public EnemySpawner enemy;

    private void Start()
    {
        userData = FindObjectOfType<UserData>();
    }

    public void PerformClickAttack()
    {
        float clickDmg = userData.clickDmg;
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, enemyLayer);
        if (hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(clickDmg);
            }
        }
    }
}

