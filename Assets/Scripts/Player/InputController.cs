using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    public InputsSO actualInputConfig;

    private void Start()
    {
        Instance = this;
    }

    public float HorizontalMovement()
    {
        return Input.GetAxis("Horizontal");
    }

    public float VerticalMovement()
    {
        return Input.GetAxis("Vertical");
    }   
    public bool JumpInput()
    {
        return Input.GetKeyDown(actualInputConfig.jump);
    }  

    public bool InteractionInput()
    {
        return Input.GetKeyDown(actualInputConfig.interact);
    }
     
    public bool ShootInput()
    {
        return Input.GetKeyDown(actualInputConfig.shoot);
    }

    public bool ReloadInput()
    {
        return Input.GetKeyDown(actualInputConfig.reload);
    }

    public Vector2 MousePos()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }
}
