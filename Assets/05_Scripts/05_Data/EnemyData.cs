using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float health;
    public int dropGold;
}
