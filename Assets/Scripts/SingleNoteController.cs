using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleNoteController : NoteControllerBase
{
    void Update()
    {
        // ノーツの座標
        Vector2 position = new Vector2();
        position.x = (noteProperty.beatBegin - PlayerController.CurrentBeat) *
       PlayerController.ScrollSpeed;
        transform.localPosition = position;
        position.y = noteProperty.lane - 2;
    }
}