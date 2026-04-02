using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "SkillSystem/EffectConfig")]
public class EffectConfig : ScriptableObject
{
    public string codeName;
    public float duration;
    public EffectActiveEvent effectActiveEvent;
    public float value;
    public float interval;

    public StatType targetStatType;
}