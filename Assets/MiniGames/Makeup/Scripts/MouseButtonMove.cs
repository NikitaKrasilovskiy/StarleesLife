using UnityEngine;
using UnityEngine.EventSystems;

public class MouseButtonMove : EventTrigger
{
    public int index;
    private Vector3 startPosicion;
    public bool dragging;
    void Start()
    { startPosicion = GetComponent<Transform>().position; }
    void Update()
    {
        if (dragging)
        { transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y); }
        else this.transform.position = startPosicion;
    }
    public override void OnPointerDown(PointerEventData eventData)
    { dragging = true; }
    public override void OnPointerUp(PointerEventData eventData)
    { dragging = false; }
}