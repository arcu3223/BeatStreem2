using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleNoteController : NoteControllerBase
{
    [SerializeField] AudioClip clipHit; // 効果音
    Transform[] StartLine = new Transform[15];
    Transform[] GoalLine = new Transform[15];

    void Start()
    {
        for (int Number = 0; Number < 15; Number++)
        {
            StartLine[Number] = GameObject.Find("StartLine (" + Number + ")").transform;
            GoalLine[Number] = GameObject.Find("GoalLine (" + Number + ")").transform;
        }
    }

    void Update()
    {
        SetTransform();
        CheckMiss();
    }

    private void SetTransform()
    {
        // ノーツの座標
        float present_Location = (noteProperty.beatBegin - PlayerController.CurrentBeat) *
        PlayerController.ScrollSpeed;

        // オブジェクトの移動
        if (this.noteProperty.lane == 0)
        {
            transform.position = Vector2.Lerp(StartLine[0].position, GoalLine[0].position, present_Location);
        }

        else if (this.noteProperty.lane == 1)
        {
            transform.position = Vector2.Lerp(StartLine[1].position, GoalLine[1].position, present_Location);
        }

        else if (this.noteProperty.lane == 2)
        {
            transform.position = Vector2.Lerp(StartLine[2].position, GoalLine[2].position, present_Location);
        }

        else if (this.noteProperty.lane == 3)
        {
            transform.position = Vector2.Lerp(StartLine[3].position, GoalLine[3].position, present_Location);
        }

        else if (this.noteProperty.lane == 4)
        {
            transform.position = Vector2.Lerp(StartLine[4].position, GoalLine[4].position, present_Location);
        }

        else if (this.noteProperty.lane == 5)
        {
            transform.position = Vector2.Lerp(StartLine[5].position, GoalLine[5].position, present_Location);
        }

        else if (this.noteProperty.lane == 6)
        {
            transform.position = Vector2.Lerp(StartLine[6].position, GoalLine[6].position, present_Location);
        }

        else if (this.noteProperty.lane == 7)
        {
            transform.position = Vector2.Lerp(StartLine[7].position, GoalLine[7].position, present_Location);
        }

        else if (this.noteProperty.lane == 8)
        {
            transform.position = Vector2.Lerp(StartLine[8].position, GoalLine[8].position, present_Location);
        }

        else if (this.noteProperty.lane == 9)
        {
            transform.position = Vector2.Lerp(StartLine[9].position, GoalLine[9].position, present_Location);
        }

        else if (this.noteProperty.lane == 10)
        {
            transform.position = Vector2.Lerp(StartLine[10].position, GoalLine[10].position, present_Location);
        }

        else if (this.noteProperty.lane == 11)
        {
            transform.position = Vector2.Lerp(StartLine[11].position, GoalLine[11].position, present_Location);
        }

        else if (this.noteProperty.lane == 12)
        {
            transform.position = Vector2.Lerp(StartLine[12].position, GoalLine[12].position, present_Location);
        }

        else if (this.noteProperty.lane == 13)
        {
            transform.position = Vector2.Lerp(StartLine[13].position, GoalLine[13].position, present_Location);
        }

        else if (this.noteProperty.lane == 14)
        {
            transform.position = Vector2.Lerp(StartLine[14].position, GoalLine[14].position, present_Location);
        }

    }

    // 見逃し検出
    private void CheckMiss()
    {
        // 判定ラインを通過した後、BADの判定幅よりも離れるとノーツを削除
        if (noteProperty.secBegin - PlayerController.CurrentSec <
        -JudgementManager.JudgementWidth[JudgementType.Bad])
        {
            // ミス処理
            EvaluationManager.OnMiss();
            // 未処理ノーツ一覧から削除
            PlayerController.ExistingNoteControllers.Remove(
            GetComponent<NoteControllerBase>()
            );
            // GameObject自体も削除
            Destroy(gameObject);
        }
    }

    // キーが押された時
    public override void OnKeyDown(JudgementType judgementType)
    {
        // デバッグ用にコンソールに判定を出力
        Debug.Log(judgementType);


        // 判定がMissでないとき(BAD以内のとき)
        if (judgementType != JudgementType.Miss)
        {
            // ヒット処理（スコア・コンボ数などを変更）
            EvaluationManager.OnHit(judgementType);
            // 効果音再生
            AudioSource.PlayClipAtPoint(clipHit, transform.position);
            // 未処理ノーツ一覧から削除
            PlayerController.ExistingNoteControllers.Remove(
            GetComponent<NoteControllerBase>()
            );
            // GameObject自体も削除
            Destroy(gameObject);
        }
    }
}