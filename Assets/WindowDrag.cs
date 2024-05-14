using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowDrag : MonoBehaviour
{
    bool Dragging = false;
    Vector2 offset = Input.mousePosition;


    private void OnMouseDown()
    {
        Dragging = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Dragging)
        {

        }
    }
}
