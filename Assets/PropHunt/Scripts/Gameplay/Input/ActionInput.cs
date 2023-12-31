using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ActionInput : MonoBehaviour
{
    [Header("Player Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;

    [Header("Properties")]
    [SerializeField] private PlayerManager _playerManager;
    private ClassInput _currentClassInput;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    public void OnMove(CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(CallbackContext context)
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            look = context.ReadValue<Vector2>();
        }
    }
    public void OnJump(CallbackContext context)
    {
        if (!context.performed) return;
        jump = true;
    }

    public void SetClassInput(ClassInput _classInput)
    {
        _currentClassInput = _classInput;
    }

    public void OnFire1(CallbackContext context)
    {
        if (!context.performed) return;
        // _currentClassInput.Fire1();
        if (_playerManager.isHunter.Value)
        {
            Debug.Log("C'est le hunter. Il faut tirer !");
            _playerManager._Fire();
        }
    }

    public void OnFire2(CallbackContext context)
    {
        if (!context.performed) return;
        _currentClassInput.Fire2();
    }

    public void OnAction1(CallbackContext context)
    {
        if (!context.performed) return;
        _currentClassInput.Action1();
    }

    public void OnAction2(CallbackContext context)
    {
        if (!context.performed) return;
        _currentClassInput.Action2();
    }
    public void OnAction3(CallbackContext context)
    {
        if (!context.performed) return;
        _currentClassInput.Action3();
    }

    public void OnCancel(CallbackContext context)
    {
        if (!context.performed) return;
        _currentClassInput.Cancel();
    }

    public void OnMenuTrigger(CallbackContext context)
    {
        if (!context.performed) return;
        //_playerManager.TriggerMenu();
    }

    public void OnTeamSwap(CallbackContext context)
    {
        //TODO : Bloquer �a pendant le jeu
        if (!context.performed) return;
        _playerManager.SwapTeamServerRPC();
    }

    public void OnCursorLockToggle(CallbackContext context)
    {
        if (!context.performed) return;
        _playerManager.ToggleCursorLock();
    }



}
