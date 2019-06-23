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
    // Start is called before the first frame update
    void Start ( ) {
        input = new HandleInput (raycaster, eventSystem);
        SourceLoader.CreateDefaultSheet ( );
    }

    // Update is called once per frame
    void Update ( ) {
        btns = input.HandlInput ( );
    }

}
