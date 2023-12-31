using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace PropHunt.Gameplay
{
    public class PlayerPlacer : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            //Place the player on the position 0,0,0. Only orks because the position is client authoritative
            StartCoroutine(LaunchOnNetworkSpawn());            
        }

     

        public IEnumerator LaunchOnNetworkSpawn()
        {
            yield return new WaitForSeconds(0.9f);

            NetworkManager.Singleton.LocalClient.PlayerObject.transform.position = new Vector3(0, 1.5f, 0);
        }
    }
}