using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private float speed = 3f;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine(){
        rb.velocity = new Vector2(-2 * speed, rb.velocity.y); 
        yield return new WaitForSecondsRealtime(2);
        rb.velocity = new Vector2(2 * speed, rb.velocity.y); 
        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = new Vector2(2 * speed, rb.velocity.y);   
    }
}
