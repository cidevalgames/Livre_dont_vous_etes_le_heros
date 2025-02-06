using UnityEngine;
using UnityEngine.EventSystems;

public class MoveChapterPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 mouseDistance = Vector3.zero;

    #region Drag events
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        mouseDistance = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        transform.position = mousePosition - mouseDistance;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mouseDistance = Vector3.zero;
    }
    #endregion
}
