using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [SerializeField]
    private SkillInfo info;
    [Range(1,10)]public int level = 1;

    public float Damage => info.baseDamage + level * info.damageIncreasePerLevel;
    public float Cooldown => Mathf.Max(0, info.baseCooldown - level * info.cooldownReducePerLevel);
    
}
