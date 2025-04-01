using System.Collections;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float moveSpeed = 2f;
    public float fadeTime = 1f;

    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            transform.position = startPos + new Vector3(0, elapsed * moveSpeed, 0);
            damageText.color = new Color(1, 0, 0, 1 - (elapsed / fadeTime));
            yield return null;
        }

        Destroy(gameObject);
    }
}