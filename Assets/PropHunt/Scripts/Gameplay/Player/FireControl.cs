using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FireControl : NetworkBehaviour
{
    [SerializeField] PlayerManager _playerManager;

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
        Debug.Log(other.gameObject);

        if (other.TryGetComponent<PropController>(out PropController prop))
        {
            Debug.Log("Prop found ! -10 points");
            Debug.Log(_playerManager.getLifePoints() - 10);
            _playerManager.setLifePoints(_playerManager.getLifePoints() - 10);
        } else
        {
            Debug.Log("Not PropController");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("In Collision !");
    }


}
