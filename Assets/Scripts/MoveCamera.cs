using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float cameraSpeed;
    [SerializeField] float dragSpeed = .5f;

    bool isDragged = false;
    Vector3 mouseDistance = Vector3.zero;
    Vector3 mousePosition = Vector3.zero;

    Vector2 direction;

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

    public void OnDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDistance = mousePosition - transform.position;
        isDragged = true;
    }

    public void OnDrop()
    {
        mouseDistance = Vector3.zero;
        mousePosition = Vector3.zero;
        isDragged = false;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = direction * cameraSpeed;

        if (isDragged)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //transform.position = transform.position -(mousePosition - mouseDistance) * dragSpeed;
            //transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            direction = transform.position - (mousePosition - mouseDistance);

            GetComponent<Rigidbody2D>().velocity = direction * dragSpeed;

            direction = Vector2.zero;
        }
    }
}
