using UnityEngine;

public class ProtecterHealth : HealthBar
{
    public override void Update()
    {
        base.Update();
        if (_inCooldown && (Time.time - _lastHealTime) >= _delayBetweenHeal)
        {
            _lastHealTime = Time.time;
            if (currentHp + _healInCooldown >= _maxHP)
                _inCooldown = false;
            GetHealth(_healInCooldown);
        }
    }

    public override void Start()
    {
        base.Start();
    }

    public override void GetDamage(float damage)
    {
        if (currentHp - damage <= 0)
        {
            Cooldown();
            base.CallOnDieEvent();
        }
        else currentHp -= damage;
    }
    
    public override void SetHealth(float newHp)
    {
        currentHp = newHp;
        if (newHp <= 0)
        {
            Cooldown();
            CallOnDieEvent();
        }
    }
    
}