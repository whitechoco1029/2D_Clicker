using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public ClickAttack clickAttack;
    public AutoAttack autoAttack;
    public ParticleSystem hitEffect; // ≈∏∞› ¿Ã∆Â∆Æ

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
}
