using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Dead");
            GameManager.instance.health.TakeDamage(100);
        }
    }
}
