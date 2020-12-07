using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using System;

public class LongNoteController : NoteControllerBase
{
    [SerializeField] GameObject objBegin; // 始点部分のGameObject
    [SerializeField] GameObject objTrail; // 軌跡部分のGameObject
    [SerializeField] GameObject objEnd; // 終点部分のGameObject
    [SerializeField] AudioClip clipHit; // 効果音

    Transform[] StartLine = new Transform[15];
    Transform[] GoalLine = new Transform[15];

    public SpriteRenderer Begin;
    public SpriteRenderer Trail;
    public Material Alpha;

    public PressGesture pressGesture;
    public ReleaseGesture releaseGesture;

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

        // 始点の座標（beatBeginによる指定）
        Vector2 positionBegin = new Vector2();

        if (this.noteProperty.lane == 0)
        {
            positionBegin.x = 0;
            positionBegin.y = 0;
            objBegin.transform.localPosition = positionBegin;
        }

        else if (this.noteProperty.lane == 1)
        {
            positionBegin.x = 0;
            positionBegin.y = 0;
            objBegin.transform.localPosition = positionBegin;
            objBegin.transform.localPosition = Quaternion.Euler(0, 0, 45) * positionBegin;
        }

        else if (this.noteProperty.lane == 2)
        {
            positionBegin.x = 0;
            positionBegin.y = 0;
            objBegin.transform.localPosition = positionBegin;
            objBegin.transform.localPosition = Quaternion.Euler(0, 0, -45) * positionBegin;
        }

        else if (this.noteProperty.lane == 3)
        {
            positionBegin.x = 0;
            positionBegin.y = 0;
            objBegin.transform.localPosition = Quaternion.Euler(0, 0, 90) * positionBegin;
        }

        else if (this.noteProperty.lane == 4)
        {
            positionBegin.x = 0;
            positionBegin.y = 0;
            objBegin.transform.localPosition = Quaternion.Euler(0, 0, -90) * positionBegin;
        }

        else if (this.noteProperty.lane == 5)
        {
            positionBegin.x = 0;
            positionBegin.y = 0;
            objBegin.transform.localPosition = positionBegin;
            objBegin.transform.localPosition = Quaternion.Euler(0, 0, 180) * positionBegin;
        }

        else if (this.noteProperty.lane == 6)
        {
            positionBegin.x = 0;
            positionBegin.y = 0;
            objBegin.transform.localPosition = positionBegin;
            objBegin.transform.localPosition = Quaternion.Euler(0, 0, 135) * positionBegin;
        }

        else if (this.noteProperty.lane == 7)
        {
            positionBegin.x = 0;
            positionBegin.y = 0;
            objBegin.transform.localPosition = positionBegin;
            objBegin.transform.localPosition = Quaternion.Euler(0, 0, -135) * positionBegin;
        }

        // 終点の座標（beatEndによる指定）
        Vector2 positionEnd = new Vector2();
        if (this.noteProperty.lane == 0)
        {
            positionEnd.x = (noteProperty.beatEnd - PlayerController.CurrentBeat) *
            PlayerController.ScrollSpeed;
            positionEnd.y = 0;
            objEnd.transform.localPosition = positionEnd;
        }

        else if (this.noteProperty.lane == 1)
        {
            positionEnd.x = (noteProperty.beatEnd - PlayerController.CurrentBeat) *
            PlayerController.ScrollSpeed;
            positionEnd.y = 0;
            objEnd.transform.localPosition = positionEnd;
            objEnd.transform.localPosition = Quaternion.Euler(0, 0, 45) * positionEnd;
        }

        else if (this.noteProperty.lane == 2)
        {
            positionEnd.x = (noteProperty.beatEnd - PlayerController.CurrentBeat) *
            PlayerController.ScrollSpeed;
            positionEnd.y = 0;
            objEnd.transform.localPosition = positionEnd;
            objEnd.transform.localPosition = Quaternion.Euler(0, 0, -45) * positionEnd;
        }

        else if (this.noteProperty.lane == 3)
        {
            positionEnd.x = (noteProperty.beatEnd - PlayerController.CurrentBeat) *
            PlayerController.ScrollSpeed;
            positionEnd.y = 0;
            objEnd.transform.localPosition = Quaternion.Euler(0, 0, 90) * positionEnd;
        }

        else if (this.noteProperty.lane == 4)
        {
            positionEnd.x = (noteProperty.beatEnd - PlayerController.CurrentBeat) *
            PlayerController.ScrollSpeed;
            positionEnd.y = 0;
            objEnd.transform.localPosition = Quaternion.Euler(0, 0, -90) * positionEnd;
        }

        else if (this.noteProperty.lane == 5)
        {
            positionEnd.x = (noteProperty.beatEnd - PlayerController.CurrentBeat) *
            PlayerController.ScrollSpeed;
            positionEnd.y = 0;
            objEnd.transform.localPosition = positionEnd;
            objEnd.transform.localPosition = Quaternion.Euler(0, 0, 180) * positionEnd;
        }

        else if (this.noteProperty.lane == 6)
        {
            positionEnd.x = (noteProperty.beatEnd - PlayerController.CurrentBeat) *
            PlayerController.ScrollSpeed;
            positionEnd.y = 0;
            objEnd.transform.localPosition = positionEnd;
            objEnd.transform.localPosition = Quaternion.Euler(0, 0, 135) * positionEnd;
        }

        else if (this.noteProperty.lane == 7)
        {
            positionEnd.x = (noteProperty.beatEnd - PlayerController.CurrentBeat) *
            PlayerController.ScrollSpeed;
            positionEnd.y = 0;
            objEnd.transform.localPosition = positionEnd;
            objEnd.transform.localPosition = Quaternion.Euler(0, 0, -135) * positionEnd;
        }



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
            objBegin.transform.rotation = Quaternion.Euler(0, 0, 45);
            objTrail.transform.rotation = Quaternion.Euler(0, 0, 45);
            objEnd.transform.rotation = Quaternion.Euler(0, 0, 45);
        }

        else if (this.noteProperty.lane == 2)
        {
            transform.position = Vector2.Lerp(StartLine[2].position, GoalLine[2].position, present_Location);
            objBegin.transform.rotation = Quaternion.Euler(0, 0, -45);
            objTrail.transform.rotation = Quaternion.Euler(0, 0, -45);
            objEnd.transform.rotation = Quaternion.Euler(0, 0, -45);
        }

        else if (this.noteProperty.lane == 3)
        {
            transform.position = Vector2.Lerp(StartLine[3].position, GoalLine[3].position, present_Location);
            objBegin.transform.rotation = Quaternion.Euler(0, 0, 90);
            objTrail.transform.rotation = Quaternion.Euler(0, 0, 90);
            objEnd.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        else if (this.noteProperty.lane == 4)
        {
            transform.position = Vector2.Lerp(StartLine[4].position, GoalLine[4].position, present_Location);
            objBegin.transform.rotation = Quaternion.Euler(0, 0, -90);
            objTrail.transform.rotation = Quaternion.Euler(0, 0, -90);
            objEnd.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        else if (this.noteProperty.lane == 5)
        {
            transform.position = Vector2.Lerp(StartLine[5].position, GoalLine[5].position, present_Location);
            objBegin.transform.rotation = Quaternion.Euler(0, 0, 180);
            objTrail.transform.rotation = Quaternion.Euler(0, 0, 180);
            objEnd.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        else if (this.noteProperty.lane == 6)
        {
            transform.position = Vector2.Lerp(StartLine[6].position, GoalLine[6].position, present_Location);
            objBegin.transform.rotation = Quaternion.Euler(0, 0, 135);
            objTrail.transform.rotation = Quaternion.Euler(0, 0, 135);
            objEnd.transform.rotation = Quaternion.Euler(0, 0, 135);
        }

        else if (this.noteProperty.lane == 7)
        {
            transform.position = Vector2.Lerp(StartLine[7].position, GoalLine[7].position, present_Location);
            objBegin.transform.rotation = Quaternion.Euler(0, 0, -135);
            objTrail.transform.rotation = Quaternion.Euler(0, 0, -135);
            objEnd.transform.rotation = Quaternion.Euler(0, 0, -135);
        }

        // 軌跡部分の座標は始点と終点の中心に設定
        Vector2 positionTrail = (positionBegin + positionEnd) / 2f;
        objTrail.transform.localPosition = positionTrail;

        if (this.noteProperty.lane == 0)
        {
            objTrail.transform.localPosition = positionTrail;
        }

        else if (this.noteProperty.lane == 1)
        {
            objTrail.transform.localPosition = Quaternion.Euler(0, 0, 45) * positionTrail;
        }

        else if (this.noteProperty.lane == 2)
        {
            objTrail.transform.localPosition = Quaternion.Euler(0, 0, -45) * positionTrail;
        }

        else if (this.noteProperty.lane == 3)
        {
            objTrail.transform.localPosition = Quaternion.Euler(0, 0, 90) * positionTrail;
        }

        else if (this.noteProperty.lane == 4)
        {
            objTrail.transform.localPosition = Quaternion.Euler(0, 0, -90) * positionTrail;
        }

        else if (this.noteProperty.lane == 5)
        {
            objTrail.transform.localPosition = Quaternion.Euler(0, 0, 180) * positionTrail;
        }

        else if (this.noteProperty.lane == 6)
        {
            objTrail.transform.localPosition = Quaternion.Euler(0, 0, 135) * positionTrail;
        }

        else if (this.noteProperty.lane == 7)
        {
            objTrail.transform.localPosition = Quaternion.Euler(0, 0, -135) * positionTrail;
        }

        // 軌跡部分の拡大率は終点の始点の座標の差に設定
        Vector2 scale = objTrail.transform.localScale;
        scale.x = positionEnd.x - positionBegin.x;
        objTrail.transform.localScale = scale;
    }

    // 見逃し検出
    private void CheckMiss()
    {
        // 処理中でない状態で始点が判定ラインを通過し、
        // BADの判定幅よりも離れるとノーツを削除
        if (!isProcessed &&
        noteProperty.secBegin - PlayerController.CurrentSec <
        -JudgementManager.JudgementWidth[JudgementType.Bad])
        {
            // ミス処理（2回呼び出す）
            EvaluationManager.OnMiss(); // 始点の分
            EvaluationManager.OnMiss(); // 終点の分
            // リストから削除
            PlayerController.ExistingNoteControllers.Remove(
            GetComponent<NoteControllerBase>()
            );
            // GameObject自体も削除
            Destroy(gameObject);
        }

        // 処理中の状態で終点が判定ラインを通過し、
        // BADの判定幅よりも離れるとノーツを削除
        if (isProcessed &&
        noteProperty.secEnd - PlayerController.CurrentSec <
        -JudgementManager.JudgementWidth[JudgementType.Bad])
        {
            // ミス処理
            EvaluationManager.OnMiss();
            // 処理中フラグを解除
            isProcessed = false;
            // リストから削除
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
        // コンソールに判定を表示
        Debug.Log(judgementType);

        // 判定がMissでないとき(BAD以内のとき)
        if (judgementType != JudgementType.Miss)
        {
            // ヒット処理
            EvaluationManager.OnHit(judgementType);
            // 効果音再生
            AudioSource.PlayClipAtPoint(clipHit, transform.position);
            // 処理中フラグを付ける
            isProcessed = true;
            // 通過オブジェクトを非表示にする
            Begin.material = Alpha;
        }
    }

    // キーが離された時
    public override void OnKeyUp(JudgementType judgementType)
    {
        // 判定がBad以内のとき
        if (judgementType != JudgementType.Miss)
        {
            // ヒット処理
            EvaluationManager.OnHit(judgementType);
        }
        // 判定がMissの時(MISS)
        else
        {
            // ミス処理
            EvaluationManager.OnMiss();
        }
        // コンソールに判定を表示
        Debug.Log(judgementType);
        // 効果音再生
        AudioSource.PlayClipAtPoint(clipHit, transform.position);
        // 処理中フラグを解除
        isProcessed = false;
        //リストから削除
        PlayerController.ExistingNoteControllers.Remove(
        GetComponent<NoteControllerBase>()
        );
        // GameObject自体も削除
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        GetComponent<PressGesture>().Pressed += OnPressed;
        GetComponent<ReleaseGesture>().Released += OnReleased;
    }

    private void OnDisable()
    {
        GetComponent<PressGesture>().Pressed -= OnPressed;
        GetComponent<ReleaseGesture>().Released -= OnReleased;
    }

    private void OnPressed(object sender, EventArgs e)
    {
        // 最近傍のノーツを処理すべきタイミング(sec)
        var noteSec = noteProperty.secBegin;
        // 処理すべきタイミングと
        // 実際にキーが押されたタイミングの差の絶対値
        var differenceSec = Mathf.Abs(noteSec - PlayerController.CurrentSec);
        // 最近傍のノーツのOnKeyDownを呼び出し
        var judgementType = JudgementManager.GetJudgementType(differenceSec);

        // デバッグ用にコンソールに判定を出力
        Debug.Log(judgementType);

        // 判定がMissでないとき(BAD以内のとき)
        if (judgementType != JudgementType.Miss)
        {
            // ヒット処理
            EvaluationManager.OnHit(judgementType);
            // 効果音再生
            AudioSource.PlayClipAtPoint(clipHit, transform.position);
            // 処理中フラグを付ける
            isProcessed = true;
            // 通過オブジェクトを非表示にする
            Begin.material = Alpha;
        }
    }

    private void OnReleased(object sender, EventArgs e)
    {
        // 最近傍のノーツを処理すべきタイミング(sec)
        var noteSec = noteProperty.secEnd;
        // 処理すべきタイミングと
        // 実際にキーが押されたタイミングの差の絶対値
        var differenceSec = Mathf.Abs(noteSec - PlayerController.CurrentSec);
        // 最近傍のノーツのOnKeyUpを呼び出し
        var judgementType = JudgementManager.GetJudgementType(differenceSec);

        // 判定がBad以内のとき
        if (judgementType != JudgementType.Miss)
        {
            // ヒット処理
            EvaluationManager.OnHit(judgementType);
        }
        // 判定がMissの時(MISS)
        else
        {
            // ミス処理
            EvaluationManager.OnMiss();
        }
        // コンソールに判定を表示
        Debug.Log(judgementType);
        // 効果音再生
        AudioSource.PlayClipAtPoint(clipHit, transform.position);
        // 処理中フラグを解除
        isProcessed = false;
        //リストから削除
        PlayerController.ExistingNoteControllers.Remove(
        GetComponent<NoteControllerBase>()
        );
        // GameObject自体も削除
        Destroy(gameObject);
    }
}