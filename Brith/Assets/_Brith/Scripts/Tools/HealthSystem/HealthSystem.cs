using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;

    private int health;
    private int healthMax;

    public HealthSystem(int _healthMax)
    {
        healthMax = _healthMax;
        health = healthMax;
    }

    public int GetHealthAmount()
    {
        return health;
    }
    
    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void TakeDamage(int _damageAmount)
    {
        health -= _damageAmount;

        if(health <= 0) health = 0;
        if(OnHealthChanged != null) OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int _healthAmount)
    {
        health += _healthAmount;

        if(health >= healthMax) health = healthMax;
        if(OnHealthChanged != null) OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
