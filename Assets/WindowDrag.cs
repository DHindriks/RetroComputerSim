using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowDrag : MonoBehaviour
{
    bool Dragging = false;
    Vector2 offset = Vector2.zero;


    private void OnMouseDown()
    {
        Dragging = true;
        offset = transform.root.position + Input.mousePosition;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Dragging)
        {

        }
    }
}
