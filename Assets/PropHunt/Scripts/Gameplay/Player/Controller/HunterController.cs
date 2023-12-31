using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class HunterController : ClassController
{
    public float burstSpeed;
    public GameObject projectile;

    public GameObject myBlackScreen;
    private bool activeBlind = false;

    private PlayerManager playerManager;
    private bool isFire = false;

    private void Start()
    {
            
    }


    void OnNetworkSpawn()
    {

    }

    bool ActiveOrNotBlind()
    {
        
        Scene myscene = SceneManager.GetActiveScene();

        if (myscene.name == "Game")
        {
            Debug.Log("In Game !");
            return true;
        }
        Debug.Log("Not In Game !");
        return false;
    }


    public IEnumerator GetBlind()
    {
        
        yield return new WaitForSeconds(10);
        myBlackScreen.SetActive(false);
        Debug.Log("Not Blind");
        activeBlind = true;
    }


    private void Update()
    {

        
        
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        _camera.transform.SetParent(transform);
        _camera.transform.localPosition = new Vector3(0f, 1f, 0f);


        ResetAnimator();
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }



    [ServerRpc]
    public void OnFireServerRpc()
    {
        if (!isFire)
        {

            this.isFire = true;
            OnFireClientRpc();

        }
    }
    

    [ClientRpc]
    public void OnFireClientRpc()
    {
          if (IsOwner && this.isFire)
        {
            this.isFire = false;
        }


        this.projectile.SetActive(true);
        var transform = this.transform;
        var newProjectile = Instantiate(this.projectile);
        newProjectile.transform.position = transform.position + transform.forward * 0.6f;
        newProjectile.transform.rotation = transform.rotation;
        const int size = 1;
        newProjectile.transform.localScale *= size;
        newProjectile.GetComponent<Rigidbody>().mass = Mathf.Pow(size, 3);
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
        newProjectile.GetComponent<MeshRenderer>().material.color =
        new Color(Random.value, Random.value, Random.value, 1.0f);
    }


    public void Fire()
    {

        this.OnFireServerRpc();

    }

}
