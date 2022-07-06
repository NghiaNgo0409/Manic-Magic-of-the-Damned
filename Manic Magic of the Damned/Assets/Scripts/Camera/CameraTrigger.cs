using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> cameras;
    // Start is called before the first frame update
    void Start()
    {
        if(Camera.main) cameras[0] = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("Player")) return;
        PlayerController.instance.state = State.Pause;
        for(int i = 1; i < cameras.Count; i++)
        {
            if(i != cameras.Count - 1)
            {
                StartCoroutine(ShowCamera(cameras[i], 1.5f));
            }
            else
            {
                StartCoroutine(ShowCamera(cameras[i], 7.5f));
            }
        }
        PlayerController.instance.state = State.Normal;
    }

    IEnumerator ShowCamera(GameObject obj, float duration)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }
}
