using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public static class SourceLoader {
    #region  TEST
    public static void CreateDefaultSheet ( ) {
        string path = Application.persistentDataPath + "/Sheet/" + "DEFAULT_SHEET.json";
        FileStream fs = new FileStream (path, FileMode.Create);
        string fileContext = JsonUtility.ToJson (CreatSheet ( ));
        Debug.Log (fileContext);
        StreamWriter file = new StreamWriter (fs);
        file.Write (fileContext);
        file.Close ( );
        Debug.Log ("Finish");
    }

    static SheetInfo CreatSheet ( ) {
        SheetInfo sheet;
        sheet.SheetName = "逃げ水Test";
        sheet.Author = "Eccentric";
        sheet.CoverPath = Application.persistentDataPath + "/Cover/" + "DefaultCover.jpg";
        sheet.Music.Artist = "乃木坂46";
        sheet.Music.BPM = 120;
        sheet.Music.SongName = "逃げ水";
        sheet.Music.SongPath = Application.persistentDataPath + "/Music/" + "DefaultMusic.ogg";
        sheet.Music.Length = 10f;
        sheet.Notes = new List<NoteInfo> ( );
        for (int i = 0; i < 15; i++) {
            NoteInfo note;
            note.Time = i + 1;
            note.Pos = (EnotePos) (i % 3);
            note.Type = (ENoteType) (i % 10);
            sheet.Notes.Add (note);
        }
        return sheet;

    }
    #endregion TEST
}
