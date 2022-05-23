using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos, ypos;
    public GameObject cam;
    public float effectStrength;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        ypos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1-effectStrength));
        float dist = (cam.transform.position.x * effectStrength);
        float ydist = (cam.transform.position.y * effectStrength);
        
        // transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        transform.position = new Vector3(startPos + dist, ypos + ydist, transform.position.z);

        if(temp > startPos + length) startPos += length;
        else if(temp < startPos - length) startPos -= length;
    }
}
