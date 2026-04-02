using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewStat", menuName = "Stats/StatConfigSO")]
public class StatConfigSO : ScriptableObject
{
    public StatType type;      // Ví dụ: HP
    public float baseValue;    // Ví dụ: 100 máu

    // Hàm đẻ ra cái hộp trong sơ đồ của ông
    public StatConfigBase CreateRuntimeStat()
    {
        return new StatConfigBase(this);
    }
}
