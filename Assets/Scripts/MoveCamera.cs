using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float cameraSpeed;
    Vector2 direction;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = direction * cameraSpeed;
    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        direction = callback.ReadValue<Vector2>();
    }

    public void OnCenter(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
