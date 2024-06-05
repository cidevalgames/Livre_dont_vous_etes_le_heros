using UnityEngine;
//using UnityEngine.EventSystems;

public class MoveChapterPanel : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    bool isDragged = false;
    Vector3 mouseDistance = Vector3.zero;

    //public void OnBeginDrag(PointerEventData eventData)
    //{

    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.position = eventData.position;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
        
    //}

    public void OnDrag()
    {
        isDragged = true;
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        mouseDistance = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0);
    }

    public void OnDrop()
    {
        isDragged = false;
        mouseDistance = Vector3.zero;
    }

    private void Update()
    {
        if (isDragged)
        {
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            transform.position = mousePosition - mouseDistance;
        }
    }
}
