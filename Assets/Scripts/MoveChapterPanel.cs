using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChapterPanel : MonoBehaviour
{
    bool isDragged = false;
    Vector3 mouseDistance = Vector3.zero;

    public void OnDrag()
    {
        isDragged = true;
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        mouseDistance = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0);

        print("dragged");
    }

    public void OnDrop()
    {
        isDragged = false;
        mouseDistance = Vector3.zero;

        print("dropped");
    }

    private void FixedUpdate()
    {
        if (isDragged)
        {
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            transform.position = mousePosition - mouseDistance;
        }
    }
}
