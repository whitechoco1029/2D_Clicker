using UnityEngine;

public class GoldCheat : MonoBehaviour
{
    void Update()
    {
        // Shift + F �Է� ����
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            float goldToAdd = 1_000_000_000_000f; // 1000��

            // GameManager�� ���� �߾� ������ ����
            if (GameManager.Instance != null && GameManager.Instance.userData != null)
            {
                GameManager.Instance.userData.gold += goldToAdd;
                Debug.Log($"ġƮŰ �ߵ�! +{goldToAdd:N0} G (���� ����: {GameManager.Instance.userData.gold:N0})");
            }
            else
            {
                Debug.LogWarning("GameManager �Ǵ� UserData�� ����Ǿ� ���� �ʽ��ϴ�.");
            }
        }
    }
}
