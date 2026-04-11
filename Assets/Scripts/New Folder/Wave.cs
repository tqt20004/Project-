using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "Data/Wave")]

public class Wave : ScriptableObject
{
    public int waveID;
    public int count;
    public RoleConfig enemyRoleCfg;

}
