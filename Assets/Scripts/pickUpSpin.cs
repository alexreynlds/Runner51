using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpSpin : MonoBehaviour
{
    public AudioSource soundEffect;

    public float amplitude = 0.25f;
    public float frequency = 1f;
    private GameObject pickUp;

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();

    // Update is called once per frame
    void Start(){
        pickUp = this.gameObject;
        posOffset = transform.position;
    }
    void Update()
    {
        pickUp.transform.Rotate(0.0f, 0.2f, 0.0f);
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = tempPos;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player"){
            if(pickUp.gameObject.tag == "Atom"){
                other.gameObject.GetComponent<playerController>().atoms++;
                soundEffect.Play();
            }
            if(pickUp.gameObject.tag == "Life"){
                GameObject.Find("Player").GetComponent<playerController>().lives++;
                soundEffect.Play();
            }
            Destroy(soundEffect.gameObject, 1f);
            Destroy(pickUp, 0.05f);
        }
    }
}
