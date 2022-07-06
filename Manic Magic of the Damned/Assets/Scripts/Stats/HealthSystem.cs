using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] Flash flashEffect;
    

    public float CurrentHealth { get => currentHealth;}
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartHealth()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth  = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        GameManager.instance.soundManager.PlaySFX(3);
        flashEffect.Flashing();
    }

    public virtual void UpdateHealthUI()
    {
        
    }

    public void GetHealthKey()
    {
        currentHealth = PlayerPrefs.GetFloat("HealthKey", maxHealth);
    }
}
