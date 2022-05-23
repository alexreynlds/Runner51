using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCamera : MonoBehaviour
{
    private float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float newPos = transform.position.x*1.1;
        // transform.position = new Vector3(transform.position.x+0.01d, transform.position.y, transform.position.y);
        transform.position += Vector3.right* (speed/2) * Time.deltaTime;
    }
}
