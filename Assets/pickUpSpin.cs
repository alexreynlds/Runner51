using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpSpin : MonoBehaviour
{
    float bobSpeed = 5f;
    float bobHeight = 0.5f;
    private GameObject atom;

    // Update is called once per frame
    void Start(){
        atom = this.gameObject;
    }
    void Update()
    {
        atom.transform.Rotate(0.0f, 0.2f, 0.0f);
        Vector3 pos = atom.transform.position;
        float newY = Mathf.Sin(Time.time * bobSpeed);
        // atom.transform.position = new Vector3(atom.transform.position.x, newY, atom.transform.position.z) * bobHeight;
    }
}
