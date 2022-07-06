using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelListManager : MonoBehaviour
{
    [SerializeField]List<GameObject> levelLockButtons;
    int[] levelPassed = new int[10];
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level"+ 0, 1);
        for(int i = 0; i < levelPassed.Length; i++)
        {
            levelPassed[i] = PlayerPrefs.GetInt("Level" + i, 0);
        }

        UpdateLevel();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateLevel()
    {
        for(int i = 0; i < levelLockButtons.Count; i++)
        {
            if(levelPassed[i] == 1)
            {
                levelLockButtons[i].SetActive(false);
            }
            else
            {
                levelLockButtons[i].SetActive(true);
            }
        }
    }
}
