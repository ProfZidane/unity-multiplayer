using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // OnTriggerEnter();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision !");
        Debug.Log(other.gameObject.tag);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter !");
    }

}
