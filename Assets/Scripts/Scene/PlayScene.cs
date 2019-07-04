using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayScene : Scene {
    [SerializeField]
    GraphicRaycaster raycaster;
    [SerializeField]
    EventSystem eventSystem;
    HandleInput input;
    BtnState btns;
    public RawImage cover;
    public AudioSource aFx;
    public SheetData data;
    public SheetInfo sheet;
    [SerializeField]
    ObjectPool noteObjectPool;
    Queue<NoteInfo> playingNotes;
    Queue<NoteInfo> rightNotes = new Queue<NoteInfo> ( );
    Queue<NoteInfo> midNotes = new Queue<NoteInfo> ( );
    Queue<NoteInfo> leftNotes = new Queue<NoteInfo> ( );
    [SerializeField]
    NoteInfo leftNote = new NoteInfo (0f);
    [SerializeField]
    NoteInfo midNote = new NoteInfo (0f);
    [SerializeField]
    NoteInfo rightNote = new NoteInfo (0f);

    // Start is called before the first frame update
    void Start ( ) {
        input = new HandleInput (raycaster, eventSystem);
        noteObjectPool.Init ( );
        SetUIAndGetSheet ("DEFAULT");
        aFx.Play ( );
    }

    // Update is called once per frame
    void Update ( ) {
        btns = input.HandlInput ( );
        GameManager.Instance.MusicTime = aFx.time;
        if (noteObjectPool.IsExist && playingNotes.Count > 0)
            SetNote ( );
        GetDetectNote ( );
        if (input.TouchCount > 0) {
            if (btns.LeftBtn.phase != TouchPhase.Ended && btns.LeftBtn.deltaTime != 0f) {
                if (Mathf.Abs (leftNote.Time - GameManager.Instance.MusicTime) <= GameManager.Instance.Data.JUGE_TIME [(int) EHitJuge.MISS]) {
                    EHitJuge juge = JungeInput (leftNote.Time, leftNote.Type);
                    Debug.Log ("L: " + juge.ToString ( ) + " MT: " + GameManager.Instance.MusicTime +" NT:" +leftNote.Time);
                    leftNote = new NoteInfo (0f);
                }
            }
            if (btns.MidBtn.phase != TouchPhase.Ended && btns.MidBtn.deltaTime != 0f) {
                if (Mathf.Abs (midNote.Time - GameManager.Instance.MusicTime) <= GameManager.Instance.Data.JUGE_TIME [(int) EHitJuge.MISS]) {
                    EHitJuge juge = JungeInput (midNote.Time, midNote.Type);
                    Debug.Log ("M: " + juge.ToString ( )+ " MT: " + GameManager.Instance.MusicTime +" NT:" +midNote.Time);
                    midNote = new NoteInfo (0f);
                }
            }
            if (btns.RightBtn.phase != TouchPhase.Ended && btns.RightBtn.deltaTime != 0f) {
                if (Mathf.Abs (rightNote.Time - GameManager.Instance.MusicTime) <= GameManager.Instance.Data.JUGE_TIME [(int) EHitJuge.MISS]) {
                    EHitJuge juge = JungeInput (rightNote.Time, rightNote.Type);
                    Debug.Log ("R: " + juge.ToString ( )+ " MT: " + GameManager.Instance.MusicTime +" NT:" +rightNote.Time);
                    rightNote = new NoteInfo (0f);

                }
            }
        }
    }

    void SetUIAndGetSheet (string sheetName) {
        data = SourceLoader.GetSheetData (sheetName);
        sheet = SourceLoader.GetSheet (data.SheetInfo);
        aFx.clip = data.Music;
        cover.texture = data.Cover;
        playingNotes = new Queue<NoteInfo> (sheet.Notes);
        for (int i = 0; i < noteObjectPool.Capacity - 1; i++)
            SetNote ( );
    }
    void SetNote ( ) {
        if (noteObjectPool.IsExist) {
            NoteInfo noteInfo = playingNotes.Dequeue ( );
            Note noteObj = noteObjectPool.GetPooledObject ( ) as Note;
            noteObj.SetData (noteInfo);
            noteObj.OnRecycle += RecycleNote;
            switch (noteInfo.Pos) {
                case EnotePos.L:
                    leftNotes.Enqueue (noteInfo);
                    break;
                case EnotePos.M:
                    midNotes.Enqueue (noteInfo);
                    break;
                case EnotePos.R:
                    rightNotes.Enqueue (noteInfo);

                    break;

            }
        }
    }

    void GetDetectNote ( ) {
        if (leftNote.Time == 0f && leftNotes.Count > 0)
            leftNote = leftNotes.Dequeue ( );
        if (rightNote.Time == 0f && rightNotes.Count > 0)
            rightNote = rightNotes.Dequeue ( );
        if (midNote.Time == 0f && midNotes.Count > 0)
            midNote = midNotes.Dequeue ( );
    }

    EHitJuge JungeInput (float time, ENoteType type) {
        float offset = Mathf.Abs (time - GameManager.Instance.MusicTime);
        if (offset < GameManager.Instance.Data.JUGE_TIME [(int) EHitJuge.PERFECT])
            return EHitJuge.PERFECT;
        else if (offset < GameManager.Instance.Data.JUGE_TIME [(int) EHitJuge.GREAT])
            return EHitJuge.GREAT;
        else if (offset < GameManager.Instance.Data.JUGE_TIME [(int) EHitJuge.GOOD])
            return EHitJuge.GOOD;
        else if (offset < GameManager.Instance.Data.JUGE_TIME [(int) EHitJuge.BAD])
            return EHitJuge.BAD;
        else
            return EHitJuge.MISS;
    }

    void RecycleNote (Note note) {
        // switch (note.Info.Pos) {
        //     case EnotePos.L:
        //         if (leftNotes.Count > 0)
        //             leftNotes.Dequeue ( );
        //         break;
        //     case EnotePos.M:
        //         if (midNotes.Count > 0)
        //             midNotes.Dequeue ( );
        //         break;
        //     case EnotePos.R:
        //         if (rightNotes.Count > 0)
        //             rightNotes.Dequeue ( );

        //         break;

        // }
        switch (note.Info.Pos) {
            case EnotePos.L:
                if (leftNotes.Count > 0)
                    leftNote = leftNotes.Dequeue ( );
                break;
            case EnotePos.M:
                if (midNotes.Count > 0)
                    midNote = midNotes.Dequeue ( );
                break;
            case EnotePos.R:
                if (rightNotes.Count > 0)
                    rightNote = rightNotes.Dequeue ( );
                break;

        }
    }

}
