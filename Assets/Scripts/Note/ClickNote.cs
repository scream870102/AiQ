using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>ClickNote define all the action of CLICK TYPE</summary>
public class ClickNote : Note {
    //the min time for judge range
    float minTime;
    //the max time for judge range
    float maxTime;
    //calculate the range of judge time
    protected override void InitData ( ) {
        minTime = Info.Time - GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.MISS];
        maxTime = Info.Time + GameManager.Instance.Data.JUDGE_TIME [(int) EHitJudge.MISS];

    }
    /// <summary>Define how clickNote judge</summary>
    /// <param name="touch">the input data for this note position</param>
    public override void Judge (Touch touch) {
        //The currently time is in the range for click note judge
        //Will judge the score and return it
        if (GameManager.Instance.MusicTime < maxTime || GameManager.Instance.MusicTime > minTime) {
            if (touch.phase == TouchPhase.Began) {
                result = TimeJudge (Mathf.Abs (GameManager.Instance.MusicTime - Info.Time));
                Recycle ( );
            }
        }
    }

}
