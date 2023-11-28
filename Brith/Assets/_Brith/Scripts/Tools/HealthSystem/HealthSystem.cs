using System;

public class HealthSystem
{
    public event Action OnHealthChanged;
    public event Action OnMaxHealthChanged;

    private int health;
    private int healthMax;  // 當前生命最大值


    public HealthSystem(int _healthMax)
    {
        healthMax = _healthMax;

        health = _healthMax;
    }

    public int GetHealthAmount()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return healthMax;
    }

    /// <summary>
    /// 最大生命值變化
    /// </summary>
    /// <param name="_healthAmount"> 增加的數值 </param>
    public void ChangeMaxHealth(int _healthAmount)
    {
        healthMax += _healthAmount;
        health = healthMax;

        Heal(_healthAmount);
    }

    
    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void TakeDamage(int _damageAmount)
    {
        health -= _damageAmount;

        if(health <= 0) health = 0;
        if(OnHealthChanged != null) OnHealthChanged?.Invoke();
    }

    public void Heal(int _healthAmount)
    {
        health += _healthAmount;

        if(health >= healthMax) health = healthMax;
        if(OnHealthChanged != null) OnHealthChanged?.Invoke();
    }
}
