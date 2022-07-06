using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Config/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] public float health;
}
