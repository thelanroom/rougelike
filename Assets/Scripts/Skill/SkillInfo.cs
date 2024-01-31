using System;
using UnityEngine;

[CreateAssetMenu]
public class SkillInfo : ScriptableObject
{
    [Range(1f,100f)]public float baseDamage;
    [Range(0f, 20f)] public float baseCooldown;
    [Range(1f, 10f)] public float damageIncreasePerLevel;
    [Range(0, 1f)] public float cooldownReducePerLevel; 
}
