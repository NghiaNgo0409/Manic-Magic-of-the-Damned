using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    [SerializeField] Animator bossAnim;
    [SerializeField] float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        StartCoroutine(CallingBoss());    
    }

    IEnumerator CallingBoss()
    {
        yield return new WaitForSeconds(delay);
        bossAnim.SetBool("isChasing", true);
    }
}
