using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Config/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float runningSpeed;
    public float rollingSpeed;
    public float climbingSpeed;
    public float jumpForce;
    public float maxHealth;
}
