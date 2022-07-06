using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<EnemyController> enemies = new List<EnemyController>();
    public UIManager menu;
    public HealthSystem health;
    public SoundManager soundManager;
    public CoinSystem coinSystem;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void RegisterUI(UIManager ui)
    {
        if(instance == null) return;
        instance.menu = ui;
    }

    public static void RegisterHealth(HealthSystem healthSystem)
    {
        if(instance == null) return;
        instance.health = healthSystem;
    }

    public static void RegisterCoin(CoinSystem coinSystem)
    {
        if(instance == null) return;
        instance.coinSystem = coinSystem;
    }

    public static void RegisterSound(SoundManager sound)
    {
        if(instance == null) return;
        instance.soundManager = sound;
    }

    public static void RegisterEnemy(EnemyController enemy)
    {
        if(instance == null) return;
        if(!instance.enemies.Contains(enemy))
        {
            instance.enemies.Add(enemy);
        }
    }

    public static void RemoveEnemy(EnemyController enemy)
    {
        if(instance == null) return;
        instance.enemies.Remove(enemy);
    }

    public static void RestartLevel()
    {
        if(instance == null) return;
        PlayerController.instance.gameObject.SetActive(false);
        instance.menu.OpenCanvas("Defeat");
        instance.enemies.Clear();
    }

    public static void RestartStats()
    {
        instance.health.RestartHealth();
        instance.health.UpdateHealthUI();
        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.GetComponent<CapsuleCollider2D>().enabled = true;
        PlayerController.instance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        PlayerController.instance.state = State.Normal;
        if(PlayerController.instance.weaponManager.weaponState == WeaponState.Sword)
        {
            PlayerController.instance.SwapToSword();
        }
        else if(PlayerController.instance.weaponManager.weaponState == WeaponState.Bow)
        {
            PlayerController.instance.SwapToBow();
        }
        else if(PlayerController.instance.weaponManager.weaponState == WeaponState.Spear)
        {
            PlayerController.instance.SwapToSpear();
        }
        else
        {
            PlayerController.instance.SwapToBase();
        }
        instance.coinSystem.CoinPicked = 0;
    }

    public void ResumeLevel()
    {
        PlayerController.instance.state = State.Normal;
        instance.menu.TurnOffObjects();
    }

    public void DestroyObjects()
    {
        if(!PlayerController.instance) Destroy(PlayerController.instance.gameObject);
        if(!CameraSingleton.instance) Destroy(CameraSingleton.instance.gameObject);
        if(!CanvasSingleton.instance) Destroy(CanvasSingleton.instance.gameObject);
    }
}
