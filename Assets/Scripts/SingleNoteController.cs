using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleNoteController : NoteControllerBase
{

    //スタートとゴールの設定
    public Transform StartPosition;
    public Transform GoalPosition;


    // スピードの設定
    public float speed = PlayerController.ScrollSpeed;

    //2点間の距離を入れる
    private float distance_two;

    void Start()
    {
        //二点間の距離を代入
        distance_two = Vector2.Distance(StartPosition.position, GoalPosition.position);
    }

    void Update()
    {
        // ノーツの座標
        float present_Location = (Time.time * speed) / distance_two;
        // オブジェクトの移動
        transform.position = Vector2.Lerp(StartPosition.position, GoalPosition.position, present_Location);
    }
}