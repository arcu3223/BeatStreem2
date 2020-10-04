using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement; // シーン遷移に必要
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject prefabSingleNote; // 生成するPrefab
    [SerializeField] private GameObject prefabBlueSingleNote; // 生成するPrefab
    [SerializeField] private GameObject prefabYellowSingleNote; // 生成するPrefab
    [SerializeField] private GameObject prefabLongNote; // 生成するPrefab
    [SerializeField] private GameObject prefabBlueLongNote; // 生成するPrefab
    [SerializeField] private GameObject prefabYellowLongNote; // 生成するPrefab
    [SerializeField] private GameObject prefabAppearNote; // 生成するPrefab
    [SerializeField] private GameObject prefabBlueAppearNote; // 生成するPrefab
    [SerializeField] private GameObject prefabYellowAppearNote; // 生成するPrefab
    [SerializeField] private GameObject prefabSlashNote; // 生成するPrefab
    [SerializeField] private GameObject prefabBlueSlashNote; // 生成するPrefab
    [SerializeField] private GameObject prefabYellowSlashNote; // 生成するPrefab
    [SerializeField] private GameObject prefabWheelNote; // 生成するPrefab
    [SerializeField] private GameObject prefabBlueWheelNote; // 生成するPrefab
    [SerializeField] private GameObject prefabYellowWheelNote; // 生成するPrefab
    [SerializeField] private GameObject prefabStreamNote; // 生成するPrefab
    [SerializeField] AudioSource audioSource; // 音源再生用AudioSource
    [SerializeField] VideoPlayer VideoPlayer; // 動画再生用VideoPlayer

    public static float ScrollSpeed = 0.1f; // 譜面のスクロール速度
    public static float CurrentSec = 0f; // 現在の経過時間(秒)
    public static float CurrentBeat = 0f; // 現在の経過時間(beat)
                                          // まだ判定処理で消えていないノーツ一覧
    public static List<NoteControllerBase> ExistingNoteControllers;

    public static Beatmap beatmap; // 譜面データを管理する
    private float startOffset = 1.0f; // 譜面のオフセット(秒)
    private float startSec = 0f; // 譜面再生開始秒数(再生停止用)
    private bool isPlaying = false; // 譜面停止中か否か

    void Awake()
    {
        // 値を初期化
        CurrentSec = 0f;
        CurrentBeat = 0f;

        // 未処理ノーツ一覧を初期化
        ExistingNoteControllers = new List<NoteControllerBase>();

        // ここの譜面読み込み処理を削除した

        // ノーツの生成を行う
        foreach (var noteProperty in beatmap.noteProperties)
        {

            GameObject lane = new GameObject();

            lane = GameObject.Find("JudgeNoteLine (" + noteProperty.lane + ")");

            // beatmapのnotePropertiesの各要素の情報からGameObjectを生成
            GameObject objNote = null;
            switch (noteProperty.noteType)
            {
                case NoteType.Single:
                    objNote = Instantiate(prefabSingleNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.BlueSingle:
                    objNote = Instantiate(prefabBlueSingleNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.YellowSingle:
                    objNote = Instantiate(prefabYellowSingleNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.Long:
                    objNote = Instantiate(prefabLongNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.BlueLong:
                    objNote = Instantiate(prefabBlueLongNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.YellowLong:
                    objNote = Instantiate(prefabYellowLongNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.Appear:
                    objNote = Instantiate(prefabAppearNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.BlueAppear:
                    objNote = Instantiate(prefabBlueAppearNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.YellowAppear:
                    objNote = Instantiate(prefabYellowAppearNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.Slash:
                    objNote = Instantiate(prefabSlashNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.BlueSlash:
                    objNote = Instantiate(prefabBlueSlashNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.YellowSlash:
                    objNote = Instantiate(prefabYellowSlashNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.Wheel:
                    objNote = Instantiate(prefabWheelNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.BlueWheel:
                    objNote = Instantiate(prefabBlueWheelNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.YellowWheel:
                    objNote = Instantiate(prefabYellowWheelNote, lane.transform.position, Quaternion.identity);
                    break;
                case NoteType.Stream:
                    objNote = Instantiate(prefabStreamNote, lane.transform.position, Quaternion.identity);
                    break;
            }
            // ノーツ生成時に未処理ノーツ一覧に追加
            ExistingNoteControllers.Add(objNote.GetComponent<NoteControllerBase>());
            objNote.GetComponent<NoteControllerBase>().noteProperty = noteProperty;
        }

        // 音源読み込み
        //StartCoroutine(LoadAudioFile(beatmap.audioFilePath));

        VideoPlayer.Prepare();

    }

    void Update()
    {
        // 譜面停止中にスペースを押したとき
        if (!isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            // 譜面再生
            isPlaying = true;
            //Debug.Log(AudioSettings.dspTime + startOffset + beatmap.audioOffset);
            // 指定した秒数待って音源再生
            audioSource.PlayScheduled(
            AudioSettings.dspTime + startOffset + beatmap.audioOffset
           );
            // 指定した秒数待って動画再生
            //VideoPlayer.time = f;
            //VideoPlayer.Play();
            StartCoroutine("LateVideo");
        }
        // 譜面停止中
        if (!isPlaying)
        {
            // startSecを更新し続ける
            startSec = Time.time;
        }
        // Escキーを押すと選曲画面に戻る
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // シーン読み込み
            SceneManager.LoadScene("SelectMusic");
        }

        // 秒数を更新
        CurrentSec = Time.time - startOffset - startSec;

        // 拍を更新(ToBeatを使用)
        CurrentBeat = Beatmap.ToBeat(CurrentSec, beatmap.tempoChanges);
    }

    // 動画を指定秒数分中断する
    private IEnumerator LateVideo()
    {
        //指定秒停止
        yield return new WaitForSeconds(1);
    
        //動画再生
        VideoPlayer.Play();
    }

    // 指定されたパスに存在する音源を読み込む
    private IEnumerator LoadAudioFile(string filePath)
    {
        // ファイルが存在しなければ処理を行わない
        if (!File.Exists(filePath)) { yield break; }
        // 音源のフォーマット種別
        var audioType = GetAudioType(filePath);
        // UnityWebRequestを用いて外部リソースを読み込む
        using (var request = UnityWebRequestMultimedia.GetAudioClip(
        "file:///" + filePath, audioType
        ))
        {
            yield return request.SendWebRequest();
            // エラーが発生しなかった場合
            if (!request.isNetworkError)
            {
                // オーディオクリップを読み込み
                var audioClip = DownloadHandlerAudioClip.GetContent(request);
                // audioSourceのclipに設定
                audioSource.clip = audioClip;
            }
        }
    }

    // ファイル名から音源のフォーマットを取得する
    private AudioType GetAudioType(string filePath)
    {
        // 拡張子を取得
        string ext = Path.GetExtension(filePath).ToLower();
        switch (ext)
        {
            case ".ogg":
                return AudioType.OGGVORBIS;
            case ".mp3":
                return AudioType.MPEG;
            case ".wav":
                return AudioType.WAV;
            default:
                return AudioType.UNKNOWN;
        }
    }
}