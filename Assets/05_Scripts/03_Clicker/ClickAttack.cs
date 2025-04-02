using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAttack : MonoBehaviour
{
    public UserData userData;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    //Debug.Log("수동 공격!");
                    enemy.TakeDamage(userData.clickDmg);
                }
            }
        }
    }
}
    
