using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpSpin : MonoBehaviour
{
    public AudioSource soundEffect;

    public float amplitude = 0.5f;
    public float frequency = 1f;
    private GameObject atom;

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();

    // Update is called once per frame
    void Start(){
        atom = this.gameObject;
        posOffset = transform.position;
    }
    void Update()
    {
        atom.transform.Rotate(0.0f, 0.2f, 0.0f);
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = tempPos;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<playerController>().Atoms++;
            soundEffect.Play();
            Destroy(soundEffect.gameObject, 1f);
            Destroy(atom, 0.2f);
        }
    }
}
