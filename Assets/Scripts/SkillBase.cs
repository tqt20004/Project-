using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillBase
{
    public AIBase owner;
    public SkillConfig skillConfig;
    private float cooldownTimer;
  

    public SkillBase(AIBase owner, SkillConfig config)
    {
        this.owner = owner;
        this.skillConfig = config;
    }

    public void OnUpdate()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
    }

    public bool CheckCondition()
    {
        if (cooldownTimer > 0) return false;
        if (owner.target == null) return false;

        // Tính khoảng cách giữa Enemy và Player
        float dist = Vector2.Distance(owner.transform.position, owner.target.transform.position);

        // Chỉ đánh khi mục tiêu nằm trong tầm (range) của Skill
        return dist <= skillConfig.range;
    }

    public void Execute()
    {
        cooldownTimer = skillConfig.cooldown;
        Debug.Log($"<color=green>[Skill Execute]</color> {owner.name} dùng chiêu: {skillConfig.codeName}");

        foreach (var efCfg in skillConfig.effects)
        {
            EffectBase robot = new EffectBase();
            robot.Init(owner.target, efCfg);
            owner.target.AddEffect(robot);
        }
    }

}   
