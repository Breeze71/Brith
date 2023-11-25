using System;
using System.Collections;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform BarPosition;
    private HealthSystem healthSystem;

    public void SetupHealthSystemUI(HealthSystem _healthSystem)
    {
        healthSystem = _healthSystem;

        healthSystem.OnHealthChanged += healthSystem_OnHealthChanged;
    }

    private void healthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        BarPosition.transform.localScale = new Vector3(healthSystem.GetHealthPercent(), 1, 1);  
    }
}
