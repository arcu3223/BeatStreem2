using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPositionController : NoteControllerBase
{
    void Update()
    {
        // ノーツの座標
        Vector2 position = new Vector2();
        position.x = noteProperty.lane;
        position.y = noteProperty.lane;
    }
}