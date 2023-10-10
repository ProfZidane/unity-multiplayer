using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class HunterController : ClassController
{
    public float burstSpeed;
    public GameObject projectile;

    public GameObject myBlackScreen;
    public float blindTime = 10;

    private bool activeBlind = false;


    private void Start()
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
    }


    private void Update()
    {

        if (ActiveOrNotBlind() && myBlackScreen.activeInHierarchy)
        {
            Debug.Log("Wait 10 sec");

            StartCoroutine(GetBlind());
        }
        
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        _camera.transform.SetParent(transform);
        _camera.transform.localPosition = new Vector3(2.5f, 1.1f, 1.2f);

        if (ActiveOrNotBlind())
        {
            myBlackScreen.SetActive(true);
        }

        ResetAnimator();
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Fire()
    {
        // var transform = this.transform;
        // var newProjectile = Instantiate(projectile);
        // newProjectile.transform.position = transform.position + transform.forward * 0.6f;
        // newProjectile.transform.rotation = transform.rotation;
        // const int size = 1;
        // newProjectile.transform.localScale *= size;
        // newProjectile.GetComponent<Rigidbody>().mass = Mathf.Pow(size, 3);
        // newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
        // newProjectile.GetComponent<MeshRenderer>().material.color =
        //     new Color(Random.value, Random.value, Random.value, 1.0f);
    }

}
