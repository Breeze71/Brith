using System;

public class HealthSystem
{
    public event Action OnHealthChanged;
    public event Action OnMaxHealthChanged;

    private int health;
    private int healthMax;  // 當前生命最大值
    private int initHealthMax;  // 初始化生命最大值


    public HealthSystem(int _healthMax)
    {
        initHealthMax = _healthMax;
        healthMax = initHealthMax;

        health = initHealthMax;
    }

    public int GetHealthAmount()
    {
        return health;
    }

    public int GetInitMaxHealth()
    {
        return initHealthMax;
    }

    public int GetCurrentMaxHealth()
    {
        return healthMax;
    }
    /// <summary>
    /// 最大生命值變化
    /// </summary>
    public void ChangeMaxHealth(int _healthAmount)
    {
        OnMaxHealthChanged?.Invoke();

        healthMax = _healthAmount;
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

        if(health >= healthMax) health = initHealthMax;
        if(OnHealthChanged != null) OnHealthChanged?.Invoke();
    }
}
