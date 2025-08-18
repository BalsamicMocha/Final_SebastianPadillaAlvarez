using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Scriptable Objects/Input Config", fileName = "New Input Config")]
public class InputsSO : ScriptableObject
{
    [Header("Movement Inputs")] 
    public KeyCode jump;

    [Header("Other Inputs")]
    public KeyCode interact;
    public KeyCode pause;

    [Header("Weapon Inputs")]
    public KeyCode shoot;
    public KeyCode reload;

}

