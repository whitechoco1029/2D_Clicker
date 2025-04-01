using UnityEngine;

public class GoldCheat : MonoBehaviour
{
    void Update()
    {
        // Shift + F 입력 감지
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            float goldToAdd = 1_000_000_000_000f; // 1000조

            // GameManager를 통해 중앙 데이터 접근
            if (GameManager.Instance != null && GameManager.Instance.userData != null)
            {
                GameManager.Instance.userData.gold += goldToAdd;
                Debug.Log($"치트키 발동! +{goldToAdd:N0} G (현재 보유: {GameManager.Instance.userData.gold:N0})");
            }
            else
            {
                Debug.LogWarning("GameManager 또는 UserData가 연결되어 있지 않습니다.");
            }
        }
    }
}
