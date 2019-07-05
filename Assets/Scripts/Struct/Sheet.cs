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
    public ENotePos Pos;
    public NoteInfo (float time, ENoteType type = ENoteType.CLICK, ENotePos pos = ENotePos.L) {
        this.Time = time;
        this.Type = type;
        this.Pos = pos;
    }

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
    SLIDE_DURATION,
    SLIDE_END,
    SLOW_IN_CLICK,
    FAST_IN_CLICK,
}

[System.Serializable]
public enum ENotePos {
    L,
    M,
    R
}

[System.Serializable]
public struct SheetData {
    public string SheetName;
    public TextAsset SheetInfo;
    public AudioClip Music;
    public Texture2D Cover;
}

[System.Serializable]
public class GlobalData {
    public List<SheetData> Sheets;
    public readonly float SPEED = 3f;
    public readonly Vector3 MOVE_DIRECTION = new Vector3 (0f, 0.1f, 1f);
    public readonly Vector3 [ ] BTN_POS = { new Vector3 (-4.55f, 0f, 0f), new Vector3 (0f, 0f, 0f), new Vector3 (4.55f, 0f, 0f) };
    public readonly float [ ] JUDGE_TIME = { 0.033f, 0.055f, 0.077f, 0.099f, 0.12f };
    public readonly float [ ] JUDGE_POINT = { 100f, 50f, 25f, 10f, 0f, 0f };
}

[System.Serializable]
public enum EHitJudge { PERFECT, GREAT, GOOD, BAD, MISS, NONE };
