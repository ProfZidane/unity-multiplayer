using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerManager : NetworkBehaviour
{
    protected MovementController _movementController;
    public Camera Camera;
    protected ClassController _currentController;
    public NetworkVariable<bool> isHunter;

    public ActionInput _actionInput;
    public Animator _animator;
    [SerializeField] PropController _propController;
    [SerializeField] HunterController _hunterController;


    private NetworkVariable<int> _lifePoint = new NetworkVariable<int>();
    private int lifePoint = 1000;
    public TMP_Text lifePrinting;

    public GameObject myBlackScreen;
    private bool activeBlind = false;
    private bool isInitialaize = false;

    private void Awake()
    {
        _movementController = GetComponent<MovementController>();
        isHunter.OnValueChanged += SwapTeam;
        this._lifePoint.OnValueChanged += LifePointChangement;
        if (_propController == null)
        {
            _propController = GetComponentInChildren<PropController>();
        }
        if(_hunterController == null)
        {
            _hunterController = GetComponentInChildren<HunterController>();
        }
        if(_actionInput == null)
        {
            _actionInput = GetComponent<ActionInput>();
        }
        if (Camera == null) Camera = GetComponentInChildren<Camera>(true);

    }


    private void Start()
    {
        // StartCoroutine(wait());
    }

    private void Update()
    {
        if (ActiveOrNotBlind() && !activeBlind && isHunter.Value && IsOwner)
        {

            myBlackScreen.SetActive(true);
            Debug.Log("Wait 10 sec");
            StartCoroutine(GetBlind());
        }


        if (SceneManager.GetActiveScene().name == "Game")
        {
            // this.SetUpLifePoint();

            //this.UpdateLifePoint(0);
        }

        
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        SwapTeam(true, false);
        if (IsOwner)
        {
            GetComponent<PlayerInput>().enabled = true;
            GetComponent<AudioListener>().enabled = true;
            _movementController.enabled = true;
            Camera.gameObject.SetActive(true);
            _movementController.SetAnimator(GetComponent<Animator>());
            return;
        }

        if (IsServer)
        {
            this._lifePoint.Value = this.lifePoint;
        }

        Camera.gameObject.SetActive(false);
    }

    [ServerRpc]
    public void SwapTeamServerRPC()
    {
        isHunter.Value = !isHunter.Value;
    }

    public void SwapTeam(bool previousIsHunterValue, bool newIsHunterValue)
    {
        if (newIsHunterValue)
        {
            _movementController.ClassController = _hunterController;
            _actionInput.SetClassInput(_hunterController.ClassInput);
            _propController.Deactivate();
            _hunterController.Activate();
            return;
        }
        _movementController.ClassController = _propController;
        _actionInput.SetClassInput(_propController.ClassInput);
        _hunterController.Deactivate();
        _propController.Activate();
    }

    public void ToggleCursorLock()
    {
        bool isLocked = !_movementController.cursorLocked;
        Cursor.lockState = isLocked? CursorLockMode.Locked : CursorLockMode.None;
        _movementController.cursorLocked = isLocked;
    }





    // Close eye


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


    public void _Fire()
    {
        this._hunterController.Fire();
    }


    public int getLifePoints()
    {
        return this.lifePoint;
    }

    public void setLifePoints(int value)
    {
        this.lifePoint = value;

        this.SetUpLifePoint(value);

        // this._lifePoint.Value = value;
        GameObject[] g = GameObject.FindGameObjectsWithTag("Scoring");
        Debug.Log("Score : " + this._lifePoint.Value);
        /*if (g.Length > 0)
        {
            this.lifePrinting = g[0].GetComponent<TMP_Text>();
            this.lifePrinting.text = "Life Point: " + lifePoint.ToString();
        }*/
    }

    public void SetUpLifePoint(int value)
    {
        if (IsServer)
        {
            this._lifePoint.Value = value;
        }
    }

    public void LifePointChangement(int previousLifePoints, int nextLifePoints)
    {
        if (nextLifePoints > 0)
        {            
            Debug.Log("N : " + nextLifePoints);
            GameObject[] g = GameObject.FindGameObjectsWithTag("Scoring");
            if (g.Length > 0)
            {

                g[0].GetComponent<TMP_Text>().text = "Life Point : " + nextLifePoints;

                Debug.Log(g[0].GetComponent<TMP_Text>().text);
            }

            return;
        }

        EditorUtility.DisplayDialog("Game Over", "Your Prop has 0 life point. Would you want to restart ?", "Yes", "No");




    }

}
