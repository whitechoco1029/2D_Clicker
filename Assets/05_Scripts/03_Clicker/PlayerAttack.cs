using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    [Header("치명타 설정")]
    public float criticalChance = 0.2f; // 20% 확률로 치명타 발생

    [Header("이펙트")]
    public ParticleSystem normalEffect;
    public ParticleSystem criticalEffect;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI 클릭 방지

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Monster")) // 몬스터를 클릭했을 때만 공격
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

        Debug.Log(isCritical ? "치명타 공격!" : "일반 공격!");
    }
}