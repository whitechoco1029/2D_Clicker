using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    [Header("ġ��Ÿ ����")]
    public float criticalChance = 0.2f; // 20% Ȯ���� ġ��Ÿ �߻�

    [Header("����Ʈ")]
    public ParticleSystem normalEffect;
    public ParticleSystem criticalEffect;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI Ŭ�� ����

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Monster")) // ���͸� Ŭ������ ���� ����
                {
                    ProcessAttack(hit.point);
                }
            }
        }
    }

    void ProcessAttack(Vector3 position)
    {
        bool isCritical = Random.value < criticalChance;
        ParticleSystem effectToSpawn = isCritical ? criticalEffect : normalEffect;

        if (effectToSpawn != null)
            Instantiate(effectToSpawn, position, Quaternion.identity);

        Debug.Log(isCritical ? "ġ��Ÿ ����!" : "�Ϲ� ����!");
    }
}