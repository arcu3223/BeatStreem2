﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProperty
{
    public float beatBegin; // 始点が判定ラインと重なるbeat
    public float beatEnd; // 終点が判定ラインと重なるbeat
    public float secBegin; // 始点が判定ラインと重なるsec
    public float secEnd; // 終点が判定ラインと重なるsec
    public int lane; // レーン
    public NoteType noteType; // ノーツ種別

    // コンストラクタ
    public NoteProperty(float beatBegin, float beatEnd, int lane, NoteType noteType)
    {
        this.beatBegin = beatBegin;
        this.beatEnd = beatEnd;
        this.lane = lane;
        this.noteType = noteType;
    }
}

public enum NoteType
{
    Single, // シングルノーツ
    BlueSingle, // 青シングルノーツ
    YellowSingle, // 黄シングルノーツ
    Long, // ロングノーツ
    BlueLong, // 青ロングノーツ
    YellowLong, // 黄ロングノーツ
    Appear, // 出現ノーツ
    BlueAppear, // 青出現ノーツ
    YellowAppear, // 黄出現ノーツ
    Slash, // スラッシュノーツ
    BlueSlash, // 青スラッシュノーツ
    YellowSlash, // 黄スラッシュノーツ
    Wheel, // スラッシュノーツ
    BlueWheel, // 青スラッシュノーツ
    YellowWheel, // 黄スラッシュノーツ
    Stream // ストリームノーツ
}