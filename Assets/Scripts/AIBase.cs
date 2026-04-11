using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class AIBase : MonoBehaviour
{
    //public InventoryPlayer inventoryPlayer;
    public RoleConfig roleConfig;
    private List<SkillBase> skills = new List<SkillBase>();
    private List<EffectBase> activeEffects = new List<EffectBase>();
    public RoleStats roleStats;
    public ITargetSelector targetSelector; //đánh quét tròn hay bắn tia Ray
    public AIBase target;
    //public IMoveMent moveMent; // Navmesh và Input 
    public SpriteRenderer spriteRenderer;
    public event Action<float, float> OnHealthChanged;
    public static event Action<Vector2> OnDie;  
    private void Awake()
    {
        roleStats = new RoleStats();
        
    }
    public void Init(RoleConfig roleC , int x)
    {
        this.roleConfig = roleC;
        roleStats.Init(roleConfig.statList);
        Debug.Log("one");
        SetSprite(); // 3. Đổi hình

        var hpStat = roleStats.GetStat(StatType.health);
        hpStat.basePercentValue = x; // đặt tạm 2 , sau này lấy từ RunTimeData
        hpStat.currentValue = hpStat.GetFinalValue();
    }
    void Start()
    {
        Debug.Log("two");
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
        //sau này tạo method if RoleX = methodY;
        ITargetSelector z = new CircleSelector();
        SetMethodGetTarget(z);

        //Add skills từ RoleConfig
        if (roleConfig == null) return;
        foreach (var cfg in roleConfig.skillList)
        {
            skills.Add(new SkillBase(this, cfg));
        }
        var hpStat = roleStats.GetStat(StatType.health);
        if (hpStat != null)
        {
            OnHealthChanged?.Invoke(hpStat.currentValue, hpStat.GetFinalValue());
        }
    }

    void Update()
    {
        CheckRay();
        // 1. Quét Skill , gắn BaseEffect connect owner ,và add vào list Effect
        foreach (var s in skills)
        {
            s.OnUpdate();
            if (s.CheckCondition())
            {
                s.Execute(); 
                // Nếu cái Skill này là loại "Đánh xong là mất dấu" (Súng Player)
                if (s.skillConfig.isResetTarget)
                {
                    target = null;
                    Debug.Log("<color=cyan>[AIBase]</color> Skill yêu cầu Reset Target!");
                }
            }
        }
        // 2. Quét Robot Effect
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            activeEffects[i].OnUpdate();
            if (activeEffects[i].IsExpired == true) activeEffects.RemoveAt(i);
        }
    }
    void CheckRay()
    {
        if(!Input.GetKeyDown(KeyCode.Space)) return;
        target = targetSelector.GetTarget(transform, 3f);
        Debug.Log(target);
        Debug.Log(roleStats.dictStats);
    }
    public void AddEffect(EffectBase eb)
    {
        activeEffects.Add(eb);
    }
    public void Apply(EffectConfig so)
    {
        Debug.Log("call Apply of : " + this.name);
        EffectBase effect = new EffectBase();
        effect.Init(this, so);  
        AddEffect(effect);
    }
    public void  SetMethodGetTarget(ITargetSelector targetSelector)
    {
        this.targetSelector = targetSelector;
    }
    public void ApplyStatEffect(StatType type, float value)
    {
        if (roleConfig.isPlayer == true) Debug.Log("this guy is Player");
        var stat = roleStats.GetStat(type);
        if (stat == null) return;

        if (type == StatType.health)
        {
            // Chỉ riêng thằng Máu mới cần tính Giáp
            float defValue = roleStats.GetStat(StatType.def)?.GetFinalValue() ?? 0;
            Debug.Log(defValue + " def Value");
            // Công thức chuẩn: Damage - Giáp (nhớ kẹp Mathf.Max để ko bị hồi máu khi giáp quá to)
            float finalDamage = Mathf.Max(0, value - (defValue * 0.1f));
            Debug.Log(finalDamage + " final");
            stat.currentValue -= finalDamage;
            OnHealthChanged?.Invoke(stat.currentValue, stat.GetFinalValue());
            if (stat.currentValue <= 0) Die();
        }
        else
        {
            // khác (Speed, Atk...) thì cứ trừ thẳng/cộng thẳng
            stat.currentValue -= value;
        }

        Debug.Log($"<color=green>[Stat Update]</color> {type} của {name} còn: {stat.currentValue}");
    }
    public void Die()
    {
        OnDie?.Invoke(transform.position);
        Destroy(transform.root.gameObject);
    }
    public void SetSprite()
    {
        if (spriteRenderer != null) { 
        spriteRenderer.sprite = roleConfig.chacracterSprite; }
    }
  
}