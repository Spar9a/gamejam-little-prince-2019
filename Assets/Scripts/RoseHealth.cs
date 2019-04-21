using UnityEngine;

public class RoseHealth : HealthBar
{

    public AudioSource Source;
    public AudioClip damageSound;
    public AudioClip deathSound;
    
    public override void Update()
    {
        base.Update();
        
    }

    public override void Start()
    {
        base.Start();
    }
    
    public override void GetDamage(float damage)
    {
        if (currentHp - damage <= 0)
        {
            CallOnDieEvent();
            DeathSound();
        }
        else
        {
            currentHp -= damage;
            DamageSound();
        }
    }

    public override void SetHealth(float newHp)
    {
        base.SetHealth(newHp);
    }

    private void DamageSound()
    {
        Source.PlayOneShot(damageSound);
        
    }

    private void DeathSound()
    {
        Source.PlayOneShot(deathSound);
    }

}