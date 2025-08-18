using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensibilidad;
    public float smoothness; // Que tan suave queremos la camara, es como un delay de lo que tarda la camara en llegar a su posicion despues de girar

    public float maxVerticalAngle;
    public float minVerticalAngle;

    public Vector2 mouseScaledPos; // Vamos a guardar el input de el mouse, y dice escalado porque asi se llama la operacion de multiplicar vectores, y este lo multiplicaremos para obtener el movimiento con sensibilidad
    public Vector2 smoothedCam; // Este vector guarda la velocidad a la que se mueve la camara de punto A a punto B
    public Vector2 camPos; // 

    public Transform player;

    public bool customLockMode;
    public CursorLockMode lockMode;

    void Start()
    {
        if (player == null)
        {
            player = transform.parent;
        }


        if (customLockMode == true)
        {
            Cursor.lockState = lockMode;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        mouseScaledPos = Vector2.Scale(InputController.Instance.MousePos(), Vector2.one * sensibilidad); // Multiplicamos el input de el mouse por la sensibilidad
        smoothedCam = Vector2.Lerp(smoothedCam, mouseScaledPos, 1 / smoothness); // Aqui estamos consiguiendo la posicion y velocidad a la que se va a mover la camara despues de mover el mouse
        camPos += smoothedCam; // Estamos sumando o restando la posicion a el vector actual

        camPos.y = Mathf.Clamp(camPos.y, minVerticalAngle, maxVerticalAngle); // Limitamos el movimiento de la camara en vertical

        transform.localRotation = Quaternion.AngleAxis(-camPos.y, Vector3.right); // Se aplica el movimiento de la camara en vertical
        player.localRotation = Quaternion.AngleAxis(camPos.x, Vector3.up); // Rotamos el player cuando movemos el mouse hacia los lados
    }
}
