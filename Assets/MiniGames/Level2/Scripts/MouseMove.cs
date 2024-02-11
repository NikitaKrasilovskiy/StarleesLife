using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public Vector3 startPosicion;
    public bool MouseDown = false;
    public int index;
    void Start()
    { startPosicion = GetComponent<Transform>().position; }
    void Update()
    {
        Vector2 Cursor = Input.mousePosition;
        Cursor = Camera.main.ScreenToWorldPoint(Cursor);
        
        if (MouseDown)
        { this.transform.position = Cursor; }
        else this.transform.position = startPosicion;
    }
    private void OnMouseDown()
    { MouseDown = true; }

    private void OnMouseUp()
    { MouseDown = false; }
}
