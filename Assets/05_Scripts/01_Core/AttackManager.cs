using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public ClickAttack clickAttack;
    public AutoAttack autoAttack;
    public ParticleSystem hitEffect; // Ÿ�� ����Ʈ

    void Update()
    {
        if (autoAttack == null)
        {
            autoAttack = gameObject.AddComponent<AutoAttack>();
            Debug.Log("AutoAttack");
        }

        if (clickAttack == null)
        {
            clickAttack = gameObject.AddComponent<ClickAttack>();
            Debug.Log("ClickAttack");
        }
    }

    //public void PlayHitEffect(Vector3 position)
    //{
    //    if (hitEffect != null)
    //    {
    //        Instantiate(hitEffect, position, Quaternion.identity);
    //    }
    //}

//    {
//    public ClickAttack clickAttack;
//    public AutoAttack autoAttack;
//    public Image targetImage; // ������ UI �̹���
//    public Color flashColor = Color.red; // ������ ����
//    private Color originalColor; // ���� ����
//    public float flashDuration = 0.1f; // �����̴� �ð�

//    void Start()
//    {
//        if (autoAttack == null)
//            autoAttack = GetComponent<AutoAttack>();

//        if (clickAttack == null)
//            clickAttack = GetComponent<ClickAttack>();

//        if (targetImage != null)
//            originalColor = targetImage.color; // ���� ���� ����
//    }

//    public void FlashUI()
//    {
//        if (targetImage != null)
//            StartCoroutine(FlashCoroutine());
//    }

//    IEnumerator FlashCoroutine()
//    {
//        targetImage.color = flashColor; // ���� ����
//        yield return new WaitForSeconds(flashDuration);
//        targetImage.color = originalColor; // ���� ���� ����
//    }
//}
}
