﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要
using UnityEngine.UI;
using TouchScript.Gestures;

public class SelectorController : MonoBehaviour
{
    [SerializeField] private Text textInformation;
    [SerializeField] private Text textScrollSpeed;

    public TapGesture tapGesture;

    // フォーマット指定文字列(テキストの初期状態から読み込み)
    private string informationTextFormat;
    private string scrollSpeedTextFormat;

    // スクロール速度
    private float scrollSpeed = 1.0f;

    // BMSファイル一覧
    private string[] beatmapPaths;
    private List<BmsLoader> bmsLoaders;
    private BmsLoader selectedBmsLoader;

    // 選択中の譜面ID
    private int selectedIndex = 0;
    // 譜面の数
    private int beatmapCount;

    private void Start()
    {
        // 譜面を検索するフォルダパス
        var beatmapDirectory = Application.dataPath + "/../Beatmaps";
        // BMSファイル一覧を取得・設定
        beatmapPaths = Directory.GetFiles(beatmapDirectory, "*.bms", SearchOption.AllDirectories);

        // 譜面情報を読み込み
        bmsLoaders = beatmapPaths.Select(path => new BmsLoader(path)).ToList();
        selectedBmsLoader = bmsLoaders[selectedIndex];
        beatmapCount = bmsLoaders.Count();

        // 初期状態のテキスト内容をフォーマットとして保持
        informationTextFormat = textInformation.text;
        scrollSpeedTextFormat = textScrollSpeed.text;

        // 初期状態でテキストを更新
        ChangeSelectedIndex(selectedIndex);
        ChangeScrollSpeed(scrollSpeed);
    }

    private void Update()
    {
        // 譜面ID変更
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelectedIndex(selectedIndex - 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSelectedIndex(selectedIndex + 1);
        }

        // スクロール速度変更
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeScrollSpeed(scrollSpeed - 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeScrollSpeed(scrollSpeed + 0.1f);
        }

        // 決定処理 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // スクロール速度を設定
            PlayerController.ScrollSpeed = scrollSpeed;
            // 譜面を設定
            PlayerController.beatmap = new Beatmap(beatmapPaths[selectedIndex]);
            // シーン切り替え
            SceneManager.LoadScene("PlayMusic");
        }
    }

    private void ChangeSelectedIndex(int newIndex)
    {
        // 譜面IDを0～譜面数-1 の間に収める
        selectedIndex = Mathf.Clamp(newIndex, 0, beatmapCount - 1);
        selectedBmsLoader = bmsLoaders[selectedIndex];

        // 楽曲情報
        var title = selectedBmsLoader.headerData["TITLE"];
        var artist = selectedBmsLoader.headerData["ARTIST"];
        var playLevel = selectedBmsLoader.headerData["PLAYLEVEL"];
        var notesCount = selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.Single) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.BlueSingle) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.YellowSingle) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.Appear) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.BlueAppear) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.YellowAppear) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.BlueSlash) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.YellowSlash) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.Stream) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.Slash) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.BlueSlash) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.YellowSlash) +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.Long) * 2 +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.BlueLong) * 2 +
        selectedBmsLoader.noteProperties
        .Count(x => x.noteType == NoteType.YellowLong) * 2;
        var minBpm = selectedBmsLoader.tempoChanges.Min(x => x.tempo);
        var maxBpm = selectedBmsLoader.tempoChanges.Max(x => x.tempo);

        // テキストを変更
        var text = string.Format(informationTextFormat,
        title, artist, playLevel, notesCount, minBpm, maxBpm
       );
        textInformation.text = text;
    }

    private void ChangeScrollSpeed(float newScrollSpeed)
    {
        // スクロール速度を0.1～10 の間に収める
        scrollSpeed = Mathf.Clamp(newScrollSpeed, 0.1f, 10f);

        // テキストを変更
        var text = string.Format(scrollSpeedTextFormat, scrollSpeed);
        textScrollSpeed.text = text;
    }

    void OnEnable()
    {
        // TapGestureのdelegateに登録
        GetComponent<TapGesture>().Tapped += OnTapped;
    }

    void OnDisable()
    {
        // 登録を解除
        GetComponent<TapGesture>().Tapped -= OnTapped;
    }

    void OnTapped(object sender, System.EventArgs e)
    {
        // スクロール速度を設定
        PlayerController.ScrollSpeed = scrollSpeed;
        // 譜面を設定
        PlayerController.beatmap = new Beatmap(beatmapPaths[selectedIndex]);
        // シーン切り替え
        SceneManager.LoadScene("PlayMusic");
    }
}