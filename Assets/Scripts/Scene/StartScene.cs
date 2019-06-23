using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class StartScene : Scene {
    void Update ( ) {
        if(Input.touchCount>0){
            GameManager.Instance.SetScene("Play");
        }
    }
}
