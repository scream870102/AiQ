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
    public NoteInfo (float time, ENoteType type = ENoteType.CLICK, EnotePos pos = EnotePos.L) {
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
    public readonly Vector3 R_BTN_POS = new Vector3 (4.55f, 0f, 0f);
    public readonly Vector3 M_BTN_POS = new Vector3 (0f, 0f, 0f);
    public readonly Vector3 L_BTN_POS = new Vector3 (-4.55f, 0f, 0f);
    public readonly float [ ] JUGE_TIME = { 0.033f, 0.055f, 0.077f, 0.099f, 0.1f };
}

public enum EHitJuge { PERFECT, GREAT, GOOD, BAD, MISS };
