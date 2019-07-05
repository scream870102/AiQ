using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Note : MonoBehaviour, IObjectPoolItem {
    [SerializeField]
    //the info in this note include time/position/type
    NoteInfo info;
    /// <summary>READONLY get the info of this note</summary>
    /// <remarks>include TIME/POSITION/TYPE</remarks>
    public NoteInfo Info { get { return info; } }
    /// <summary>what object pool does this note belongs to</summary>
    public ObjectPool Pool { get; set; }
    // the init position of this note 
    protected Vector3 initPos;
    /// <summary>Event will return judge result and self reference</summary>
    public event System.Action<Note, EHitJudge> OnRecycle;
    //the result of this note due to player input
    protected EHitJudge result = EHitJudge.NONE;
    #region CLICK_NOTE
    float minTime;
    float maxTime;
    #endregion CLICK_NOTE
    public void Recycle ( ) {
        if (OnRecycle != null)
            OnRecycle (this, result);
        Debug.Log(result);
        Pool.RecycleObject (this);

    }
    public void SetData (NoteInfo info) {
        this.info = info;
        initPos = GameManager.Instance.Data.BTN_POS [(int) info.Pos];
        transform.position = initPos + GameManager.Instance.Data.MOVE_DIRECTION * GameManager.Instance.Data.SPEED * (info.Time - GameManager.Instance.MusicTime);
        result = EHitJudge.MISS;
        InitData ( );
    }

    public void Init ( ) {
        transform.position = Vector3.zero;
    }

    
    public void Tick ( ) {
        transform.position = initPos + GameManager.Instance.Data.MOVE_DIRECTION * GameManager.Instance.Data.SPEED * (info.Time - GameManager.Instance.MusicTime);
        if (GameManager.Instance.MusicTime > maxTime) {
            Recycle ( );
        }
    }

    /// <summary>call this method to ask note get the judge</summary>
    public virtual void Judge (Touch touch) {
        #region CLICK_NOTE
        //The currently time is in the range for click note judge
        //Will judge the score and return it
        if (GameManager.Instance.MusicTime <= maxTime && GameManager.Instance.MusicTime >= minTime) {
            if (touch.phase == TouchPhase.Began && touch.deltaTime != 0) {
                result = TimeJudge (Mathf.Abs (GameManager.Instance.MusicTime - Info.Time));
                if (result != EHitJudge.NONE)
                    Recycle ( );
            }
        }
        #endregion CLICK_NOTE
    }

    //this method will call after set data
    protected virtual void InitData ( ) {
        #region CLICK_NOTE
        minTime = Info.Time - GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.MISS];
        maxTime = Info.Time + GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.MISS];

        #endregion CLICK_NOTE
    }

    //give the time offset and return how the judge it is
    //time offset =  Abs(musicTime-noteTime)
    protected EHitJudge TimeJudge (float timeOffset) {
        if (timeOffset <= GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.PERFECT])
            return EHitJudge.PERFECT;
        else if (timeOffset < GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.GREAT])
            return EHitJudge.GREAT;
        else if (timeOffset < GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.GOOD])
            return EHitJudge.GOOD;
        else if (timeOffset < GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.BAD])
            return EHitJudge.BAD;
        else if (timeOffset < GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.MISS])
            return EHitJudge.MISS;
        else
            return EHitJudge.NONE;

    }

}
