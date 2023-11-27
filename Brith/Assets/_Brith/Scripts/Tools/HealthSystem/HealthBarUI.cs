using System;
using System.Collections;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform barPosition;
    private HealthSystem healthSystem;

    public void SetupHealthSystemUI(HealthSystem _healthSystem)
    {
        healthSystem = _healthSystem;

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnMaxHealthChanged += HealthSystem_OnMaxHealthChanged;
    }
    private void OnDestroy() 
    {
        healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
        healthSystem.OnMaxHealthChanged -= HealthSystem_OnMaxHealthChanged;        
    }

    private void HealthSystem_OnMaxHealthChanged()
    {
        UpdateUI();        
    }

    private void HealthSystem_OnHealthChanged()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        float _healthPercent = healthSystem.GetHealthPercent();
        // float _maxHealthPercent = healthSystem.GetInitMaxHealth() / healthSystem.GetInitMaxHealth();

        barPosition.transform.localScale = new Vector3(_healthPercent, 1, 1);
    }
}
