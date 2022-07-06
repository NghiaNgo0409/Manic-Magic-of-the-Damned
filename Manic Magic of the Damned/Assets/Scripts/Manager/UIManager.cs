using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject settingCanvas;
    [SerializeField] GameObject levelCanvas;
    [SerializeField] GameObject quitCanvas;
    [SerializeField] GameObject titleCanvas;
    [SerializeField] GameObject defeatCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] Text winText;
    [SerializeField] Text defeatText;
    [SerializeField] CalculateStar calStart;
    [SerializeField] List<GameObject> lists;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.RegisterUI(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCanvas(string name)
    {
        TurnOffObjects();
        UpdateText();
        switch (name)
        {
            case "Setting":
                settingCanvas.SetActive(true);
                break;
            case "Level":
                levelCanvas.SetActive(true);
                break;
            case "Quit":
                quitCanvas.SetActive(true);
                break;
            case "Cancel":
                titleCanvas.SetActive(true);
                break;
            case "Defeat":
                defeatCanvas.SetActive(true);
                break;
            case "Win":
                calStart.UpdateStar();
                winCanvas.SetActive(true);
                break;
            case "Pause":
                pauseCanvas.SetActive(true);
                break;
            default:
                break;
        }
        
    }

    public void TurnOffObjects()
    {
        foreach(GameObject obj in lists)
        {
            if(obj.activeSelf)
            {
                obj.SetActive(false);
            }
        } 
    }

    void UpdateText()
    {
        if(!defeatText || !winText) return;
        winText.text = GameManager.instance.coinSystem.CoinPicked.ToString();
        defeatText.text = GameManager.instance.coinSystem.CoinPicked.ToString();
    }
}
