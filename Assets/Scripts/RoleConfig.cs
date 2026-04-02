using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRole", menuName = "SkillSystem/RoleConfig")]
public class RoleConfig : ScriptableObject
{
    public string roleName;
    public List<SkillConfig> skillList;
    public bool isPlayer;
    public List<StatConfigSO> statList;
    public Sprite chacracterSprite;
}