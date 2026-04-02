using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillSystem/SkillConfig")]
public class SkillConfig : ScriptableObject
{
    public string codeName;
    public float range;
    public float cooldown;
    public SkillActiveCondition condition;
    public List<EffectConfig> effects;
    public bool isResetTarget;
}