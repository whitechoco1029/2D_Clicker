using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageParticle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDamage;
    [SerializeField] float forceRange;
    [SerializeField] float forcePower;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(string damage, Color color)
    {
        textDamage.text = damage;
        textDamage.outlineColor = color;
        
        Vector2 force = new Vector2(Random.Range(-forceRange / 2, forceRange/ 2), forcePower);

        rigid.AddForce(force, ForceMode2D.Impulse);
    }
}
