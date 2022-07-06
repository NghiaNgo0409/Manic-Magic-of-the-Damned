using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin Splashing")]
    [SerializeField] Transform objTransform;
    float delay = 0;
    float when = 1.0f;
    Vector3 offset;
    Rigidbody2D rb;

    [Header("Coin Pickup")]
    [SerializeField] GameObject pickupVFX;

    void Awake()
    {
        offset = new Vector3(Random.Range(-2.0f, 2.0f), offset.y, offset.z);

        offset = new Vector3(offset.x, Random.Range(1.0f, 3.0f), offset.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(when >= delay)
        {
            objTransform.position += offset * Time.deltaTime;
            delay += Time.deltaTime;
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);   
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(pickupVFX, transform.position, pickupVFX.transform.rotation);
            GameManager.instance.coinSystem.IncreaseCoin(10);
            GameManager.instance.soundManager.PlaySFX(11);
            Destroy(gameObject);
        }
    }
}
