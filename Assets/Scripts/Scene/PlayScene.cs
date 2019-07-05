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
    [SerializeField]
    ObjectPool noteObjectPool;
    HandleInput input;
    BtnState btns;
    public RawImage cover;
    public AudioSource aFx;
    public SheetData data;
    public SheetInfo sheet;
    Queue<NoteInfo> queueNotes;
    Queue<Note> [ ] sceneNotes = { new Queue<Note> ( ), new Queue<Note> ( ), new Queue<Note> ( ) };
    Note [ ] detectNotes = { null, null, null };

    // Start is called before the first frame update
    void Start ( ) {
        input = new HandleInput (raycaster, eventSystem);
        noteObjectPool.Init ( );
        SetUIAndGetSheet ("DEFAULT");
        aFx.Play ( );
    }

    void FixedUpdate ( ) {
        //keep update basic information
        btns = input.HandlInput ( );
        GameManager.Instance.MusicTime = aFx.time;
        //keep check if there should any notes need to add to handle list if noteObjectPool is still exist
        if (noteObjectPool.IsExist && queueNotes.Count > 0)
            SetNote ( );
        GetDetectNote ( );
        //keep Render the note on the scene
        RenderNote ( );
        //Keep update for the note should be judged
        JudgeInput ( );
    }

    void SetUIAndGetSheet (string sheetName) {
        data = SourceLoader.GetSheetData (sheetName);
        sheet = SourceLoader.GetSheet (data.SheetInfo);
        aFx.clip = data.Music;
        cover.texture = data.Cover;
        queueNotes = new Queue<NoteInfo> (sheet.Notes);
        for (int i = 0; i < noteObjectPool.Capacity; i++)
            SetNote ( );
    }

    void SetNote ( ) {
        NoteInfo noteInfo = queueNotes.Dequeue ( );
        Note noteObj = noteObjectPool.GetPooledObject ( ) as Note;
        noteObj.SetData (noteInfo);
        noteObj.OnRecycle += RecycleNote;
        sceneNotes [(int) noteInfo.Pos].Enqueue (noteObj);
    }

    void GetDetectNote ( ) {
        for (int i = 0; i < detectNotes.Length; i++) {
            if (!detectNotes [i] && sceneNotes [i].Count > 0)
                detectNotes [i] = sceneNotes [i].Dequeue ( );
        }
    }

    void RenderNote ( ) {
        foreach (Queue<Note> notes in sceneNotes) {
            foreach (Note note in notes)
                if (note.gameObject.activeInHierarchy) note.Tick ( );
        }
        foreach (Note note in detectNotes)
            if (note) note.Tick ( );
    }

    void JudgeInput ( ) {
        foreach (Note note in detectNotes) {
            if (note) note.Judge ( );
        }
    }
    void RecycleNote (Note note) {
        note.OnRecycle -= RecycleNote;
        detectNotes [(int) note.Info.Pos] = null;
    }

}
