using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    
    private static PlayerMap _inputSystem;
    public static PlayerMap.PlayerActions PlayerActions;
    public static PlayerMap.UIActions UIActions;


    private void Awake()
    {
        _inputSystem ??= new PlayerMap();
        PlayerActions = _inputSystem.Player;
        UIActions = _inputSystem.UI;
    }

    private void OnEnable()
    {
        // _inputSystem = new PlayerMap();

        // PlayerActions = _inputSystem.Player;
        // UIActions = _inputSystem.UI;
    }
}
