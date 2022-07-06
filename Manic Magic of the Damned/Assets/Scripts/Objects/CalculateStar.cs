using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateStar : MonoBehaviour
{
    [SerializeField] List<GameObject> stars;
    [SerializeField] Sprite starEmpty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStar()
    {
        if(GameManager.instance.health.CurrentHealth <= 65 && GameManager.instance.health.CurrentHealth > 30)
        {
            stars[2].GetComponent<Image>().sprite = starEmpty;
        }
        else if(GameManager.instance.health.CurrentHealth <= 30 && GameManager.instance.health.CurrentHealth > 5)
        {
            stars[2].GetComponent<Image>().sprite = starEmpty;
            stars[1].GetComponent<Image>().sprite = starEmpty;
        }
        else if(GameManager.instance.health.CurrentHealth <= 5)
        {
            foreach(GameObject star in stars)
            {
                star.GetComponent<Image>().sprite = starEmpty;
            }
        }
        else
        {
            return;
        }
    }
}
