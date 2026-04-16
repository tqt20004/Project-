using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "Data/Wave")]

public class WaveCfg : ScriptableObject
{
    public int waveID;
    public int count;
    public RoleConfig enemyRoleCfg;

}
