using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveOnPlayer()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void LeavePlayer()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
