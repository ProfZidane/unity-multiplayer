using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnProps : MonoBehaviour
{
    public Prop PropToSpawn;

    public int minNumberOfProps = 0;
    public int maxNumberOfProps = 5;

    public List<Transform> SpawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        if (NetworkManager.Singleton == null || !NetworkManager.Singleton.IsServer)
        {
            return;
        }

        int nPropsToSpawn = UnityEngine.Random.Range(minNumberOfProps, maxNumberOfProps);

        System.Random rng = new System.Random();
        SpawnPoints.Shuffle();
        for (int i=0; i < nPropsToSpawn; i++)
        {
            var sp = SpawnPoints[i];
            var transform = PropToSpawn.transform;
            var go = Instantiate(PropToSpawn.gameObject, sp.position, transform.rotation);
            go.GetComponent<NetworkObject>().Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}


