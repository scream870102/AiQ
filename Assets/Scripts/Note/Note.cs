using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Note : MonoBehaviour, IObjectPoolItem {
    [SerializeField]
    NoteInfo info;
    public NoteInfo Info { get { return info; } }
    public ObjectPool Pool { get; set; }
    Vector3 initPos;
    public event System.Action<Note> OnRecycle;
    public void Recycle ( ) {
        if (OnRecycle != null)
            OnRecycle (this);
        Pool.RecycleObject (this);

    }
    public void SetData (NoteInfo info) {
        this.info = info;
        initPos = GameManager.Instance.Data.BTN_POS [(int) info.Pos];
        transform.position = initPos + GameManager.Instance.Data.MOVE_DIRECTION * GameManager.Instance.Data.SPEED * (info.Time - GameManager.Instance.MusicTime);
    }

    public void Init ( ) {
        transform.position = Vector3.zero;
    }

    public void Tick ( ) {
        transform.position = initPos + GameManager.Instance.Data.MOVE_DIRECTION * GameManager.Instance.Data.SPEED * (info.Time - GameManager.Instance.MusicTime);
        if (GameManager.Instance.MusicTime > info.Time) {
            Recycle ( );
        }
    }

    public void Judge(){
        
    }

}
