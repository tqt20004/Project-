using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoleStats
{
    // Dictionary đúng y hệt trong hình
    public Dictionary<StatType, StatConfigBase> dictStats = new Dictionary<StatType, StatConfigBase>();

    // Hàm Init nhận vào RoleCfg (chứa list SO)
    public void Init(List<StatConfigSO> listSO)
    {
        foreach (var so in listSO)
        {
            // Tạo ra máy tính con (StatConfigBase)
            StatConfigBase newStat = so.CreateRuntimeStat();

            // Nhét vào Dictionary
            if (!dictStats.ContainsKey(newStat.type))
            {
                dictStats.Add(newStat.type, newStat);
            }
        }
    }

    // Hàm lấy chỉ số cho AIBase dùng
    public StatConfigBase GetStat(StatType type)
    {
        if (dictStats.TryGetValue(type, out var stat)) return stat;
        return null;
    }
}
