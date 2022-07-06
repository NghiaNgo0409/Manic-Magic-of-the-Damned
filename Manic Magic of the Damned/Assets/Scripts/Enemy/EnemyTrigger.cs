using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] List<EnemyController> enemies_list_in_room = new List<EnemyController>();
    [SerializeField] Animator frontDoor;
    [SerializeField] Animator backDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (EnemyController enemy in enemies_list_in_room)
        {
            if(enemy.GetComponent<EnemyController>().isDead)
            {
                enemies_list_in_room.Remove(enemy);
            }
        }

        if(enemies_list_in_room.Count == 0) backDoor.SetBool("Switch", true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            frontDoor.SetBool("Switch", true);
        }
    }

    public void TriggerEnemy()
    {
        foreach(EnemyController enemy in enemies_list_in_room)
        {
            enemy.GetComponent<Animator>().SetBool("isChasing", true);
        }
    }
}
