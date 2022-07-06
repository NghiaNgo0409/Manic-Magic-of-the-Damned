using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string nameScene;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {

        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(string name)
    {
        if(name == "Menu")
        {
            if(!PlayerController.instance) Destroy(PlayerController.instance.gameObject);
            if(!CameraSingleton.instance) Destroy(CameraSingleton.instance.gameObject);
            if(!CanvasSingleton.instance) Destroy(CanvasSingleton.instance.gameObject);
            for(int i = 0; i < 3; i++)
            {
                if(PlayerPrefs.HasKey("CrateBroken" + i))
                {
                    PlayerPrefs.DeleteKey("CrateBroken" + i);
                }
            }
        }
        if(PlayerController.instance)
        {
            if(PlayerController.instance.state != State.Normal) PlayerController.instance.state = State.Normal;
        }
        if(GameManager.instance.health)
        {
            GameManager.instance.health.GetHealthKey();
        }

        if(GameManager.instance.coinSystem) GameManager.instance.coinSystem.CoinPicked = 0;
        ResetObjectInScene();
        
        SceneManager.LoadScene(name);
    }

    public void Restart(string password)
    {
        PlayerController.instance.sceneNewPassword = password;
        if(PlayerController.instance.state != State.Normal) PlayerController.instance.state = State.Normal;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.RestartStats();
    }

    public void ResetObjectInScene()
    {
        for(int i = 0; i < 3; i++)
        {
            if(PlayerPrefs.HasKey("CrateBroken" + i))
            {
                PlayerPrefs.SetInt("CrateBroken" + i, 0);
            }
        }
    }

    void OnApplicationQuit() 
    {
        PlayerPrefs.DeleteAll();    
    }

    public void Quit()
    {
        Application.Quit();
    }
}
