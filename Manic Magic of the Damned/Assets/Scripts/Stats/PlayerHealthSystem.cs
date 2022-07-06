using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] PlayerData data;
    [SerializeField] float slidingTime;
    [SerializeField] Image healthBar;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.RegisterHealth(this);
        maxHealth = data.maxHealth;       
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.health != null)
        {
            GameManager.RegisterHealth(this);
        }

        if(CurrentHealth <= 0)
        {
            PlayerController.instance.playerAnim.SetFloat("xVelocity", 0);
            PlayerController.instance.playerAnim.SetTrigger("Dead");
            Debug.Log("Dead");
            PlayerController.instance.GetComponent<CapsuleCollider2D>().enabled = false;
            PlayerController.instance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            PlayerController.instance.state = State.Dead;
            GameManager.instance.soundManager.PlaySFX(1);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealthUI();
    }

    public override void UpdateHealthUI()
    {
        healthBar.DOFillAmount(currentHealth/100, slidingTime);
    }
}
