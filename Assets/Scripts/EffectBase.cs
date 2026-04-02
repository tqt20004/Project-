using UnityEngine;
using System.Collections.Generic;

public class EffectBase
{
    public AIBase owner;
    public EffectConfig config;
    private float timer;
    public bool IsExpired;
    public float interval;

    public void Init(AIBase owner, EffectConfig config)
    {
        this.owner = owner;
        this.config = config;
        this.timer = config.duration;
        this.interval = config.interval;
        Debug.Log($"<color=cyan>[Robot Effect]</color> Đã tạo {config.codeName} bám lên {owner.name}");
    }

    public void OnUpdate()
    {
        //đếm ngược , nếu bé hơn 0 thì stop
        if (timer <= 0)
        {
            if (IsExpired == false)
            {
                IsExpired = true;
            }
            return;
        }
        
        timer -= Time.deltaTime;

        // Giả lập gây sát thương/hồi máu
        if (config.effectActiveEvent == EffectActiveEvent.OnInterval)
        {
            interval -= Time.deltaTime;
            if (interval <= 0)
            {
                Debug.Log($"<color=yellow>[Effect Work]</color> {config.codeName} đang 'vả' {config.value} vào {owner.name}");
                owner.ApplyStatEffect(config.targetStatType , config.value);
                interval = config.interval;
            }
        }
        if(config.effectActiveEvent == EffectActiveEvent.OnUseSkill)
        {
          
            owner.ApplyStatEffect(config.targetStatType, config.value);
            IsExpired = true;
        }
        //if (timer <= 0) Debug.Log($"<color=red>[Effect Die]</color> {config.codeName} hết hạn!");
    }
}