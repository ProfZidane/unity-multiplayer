using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static Unity.Netcode.NetworkManager;

public class ConnectionApprovalHandler : MonoBehaviour
{

    private NetworkManager m_NetworkManager;
    public int MaxNumberOfPlayers = 6;
    private int _numberOfPlayers = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_NetworkManager = GetComponent<NetworkManager>();
        if (m_NetworkManager != null)
        {
            m_NetworkManager.OnClientConnectedCallback += OnClientDisconnectCallback;
            m_NetworkManager.ConnectionApprovalCallback = checkApprovalCallback;
        }

        if (MaxNumberOfPlayers == 0)
        {
            MaxNumberOfPlayers++;
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check if gamer's numbers stay lower than 7
    void checkApprovalCallback(ConnectionApprovalRequest request, ConnectionApprovalResponse response)
    {
        bool isApproved = true;
        _numberOfPlayers++;

        if (_numberOfPlayers > MaxNumberOfPlayers)
        {
            isApproved = false;
            response.Reason = "Too many players in lobby";
        }
        response.Approved = isApproved;
        response.CreatePlayerObject = isApproved;
        response.Position = new Vector3(0,3,0);
    }

    // Print error message 
    void OnClientDisconnectCallback(ulong obj)
    {
        if (!m_NetworkManager.IsServer && m_NetworkManager.DisconnectReason != string.Empty && !m_NetworkManager.IsApproved)
        {
            Debug.Log($"Approval Declined Reason : {m_NetworkManager.DisconnectReason}");
            _numberOfPlayers--;
        }

    }

}
