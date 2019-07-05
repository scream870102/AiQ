using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HandleInput {
    GraphicRaycaster raycaster;
    EventSystem eventSystem;
    BtnState btnState;
    public BtnState Buttons { get { return btnState; } }
    public int TouchCount { get { return Input.touchCount; } }
    public HandleInput (GraphicRaycaster raycaster, EventSystem eventSystem) {
        this.raycaster = raycaster;
        this.eventSystem = eventSystem;
    }

    public BtnState HandlInput ( ) {
        foreach (Touch touch in Input.touches) {
            PointerEventData pointerEventData = new PointerEventData (eventSystem);
            pointerEventData.position = touch.position;

            List<RaycastResult> results = new List<RaycastResult> ( );

            raycaster.Raycast (pointerEventData, results);

            foreach (RaycastResult result in results) {
                switch (result.gameObject.name) {
                    case "LeftBtn":
                        btnState.LeftBtn = touch;
                        break;
                    case "MidBtn":
                        btnState.MidBtn = touch;
                        break;
                    case "RightBtn":
                        btnState.RightBtn = touch;
                        break;
                }
            }
        }
        //Debug.Log("L: "+btnState.LeftBtn.deltaTime+" M: "+btnState.MidBtn.deltaTime+" R: "+btnState.RightBtn.deltaTime);
        //Debug.Log ("L:" + btnState.LeftBtn.phase + " M:" + btnState.MidBtn.phase + " R:" + btnState.RightBtn.phase);
        return btnState;
    }
}
