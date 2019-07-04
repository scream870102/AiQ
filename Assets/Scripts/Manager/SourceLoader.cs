using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.Networking;

public class SourceLoader : MonoBehaviour {
    #region WEBREQUSET
    public AudioClip GetAudio (string path) {
        AudioClip resource = null;
        StartCoroutine (GetAudioClip (path, resource));
        return resource;
    }
    IEnumerator GetAudioClip (string path, AudioClip clip) {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip (path, AudioType.OGGVORBIS)) {
            yield return www.SendWebRequest ( );

            if (www.isNetworkError) {
                Debug.Log (www.error);
            }
            else {
                clip = DownloadHandlerAudioClip.GetContent (www);
            }
        }
    }

    public Texture2D GetCover (string path) {
        Texture2D tex = null;
        StartCoroutine (path, tex);
        return tex;
    }
    IEnumerator GetTexture (string path, Texture2D tex) {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture (path)) {
            yield return uwr.SendWebRequest ( );

            if (uwr.isNetworkError || uwr.isHttpError) {
                Debug.Log (uwr.error);
            }
            else {
                // Get downloaded asset bundle
                tex = DownloadHandlerTexture.GetContent (uwr);
            }
        }
    }

    public TextAsset GetSheetText (string path) {
        TextAsset sheet = null;
        string content = "";
        StartCoroutine (GetSheetString (path, content));
        sheet = new TextAsset (content);
        return sheet;
    }

    IEnumerator GetSheetString (string path, string sheet) {
        using (UnityWebRequest uwr = UnityWebRequest.Get (path)) {
            yield return uwr.SendWebRequest ( );

            if (uwr.isNetworkError || uwr.isHttpError) {
                Debug.Log (uwr.error);
            }
            else {
                // Get downloaded asset bundle
                sheet = uwr.downloadHandler.text;
            }
        }
    }
    #endregion WEBREQUSET
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
    public static SheetData GetSheetData (string name) {
        SheetData data = new SheetData ( );
        foreach (SheetData sheet in GameManager.Instance.Data.Sheets) {
            if (sheet.SheetName == name) {
                data = sheet;
                break;
            }
        }
        return data;
    }
    public static SheetInfo GetSheet (TextAsset textAsset) {
        string content = textAsset.text;
        SheetInfo sheet = JsonUtility.FromJson<SheetInfo> (content);
        return sheet;
    }
}
