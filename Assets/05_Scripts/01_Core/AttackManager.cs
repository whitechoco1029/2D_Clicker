using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public ClickAttack clickAttack;
    public AutoAttack autoAttack;
    public ParticleSystem hitEffect; // 타격 이펙트

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
//    public Image targetImage; // 깜빡일 UI 이미지
//    public Color flashColor = Color.red; // 깜빡일 색상
//    private Color originalColor; // 원래 색상
//    public float flashDuration = 0.1f; // 깜빡이는 시간

//    void Start()
//    {
//        if (autoAttack == null)
//            autoAttack = GetComponent<AutoAttack>();

//        if (clickAttack == null)
//            clickAttack = GetComponent<ClickAttack>();

//        if (targetImage != null)
//            originalColor = targetImage.color; // 원래 색상 저장
//    }

//    public void FlashUI()
//    {
//        if (targetImage != null)
//            StartCoroutine(FlashCoroutine());
//    }

//    IEnumerator FlashCoroutine()
//    {
//        targetImage.color = flashColor; // 색상 변경
//        yield return new WaitForSeconds(flashDuration);
//        targetImage.color = originalColor; // 원래 색상 복구
//    }
//}
}
