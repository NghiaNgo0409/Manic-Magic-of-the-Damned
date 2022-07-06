using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> coinSpawner = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("SpawnCoin")]
    public void SpawnCoin(Transform objTransform)
    {
        foreach(GameObject obj in coinSpawner)
        {
            Instantiate(obj, objTransform.position, obj.transform.rotation);
        }
        GameManager.instance.soundManager.PlaySFX(12);
    }
}
