using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] Text coinText;
    int coin;

    public int Coin { get => coin; }

    public int CoinPicked { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.RegisterCoin(this);
        coin = PlayerPrefs.GetInt("Coin", 0);
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Gold: " + coin;
    }

    public void IncreaseCoin(int amount)
    {
        coin += amount;
        CoinPicked += amount;
        PlayerPrefs.SetInt("Coin", coin);
    }
}
