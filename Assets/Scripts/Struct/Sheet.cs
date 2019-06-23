using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public struct SheetInfo {
    public string SheetName;
    public string Author;
    public string CoverPath;
    public MusicInfo Music;
    public List<NoteInfo> Notes;

}
[System.Serializable]
public struct NoteInfo {
    public float Time;
    public ENoteType Type;
    public EnotePos Pos;

}
[System.Serializable]
public struct MusicInfo {
    public string Artist;
    public string SongName;
    public string SongPath;
    public int BPM;
    public float Length;
}
[System.Serializable]
public enum ENoteType {
    CLICK,
    HOLD_START,
    HOLD_DURATION,
    HOLD_END,
    FLICK,
    SLIDE_START,
    SLIDE_DUARION,
    SLIDE_END,
    SLOW_IN_CLICK,
    FAST_IN_CLICK,
}
[System.Serializable]
public enum EnotePos {
    L,
    M,
    R
}
