using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HandleInput {
    GraphicRaycaster raycaster;
    EventSystem eventSystem;
    Touch [ ] btnStates = { new Touch ( ), new Touch ( ), new Touch ( ) };
    public Touch [ ] Buttons { get { return btnStates; } }
    public int TouchCount { get { return Input.touchCount; } }
    public HandleInput (GraphicRaycaster raycaster, EventSystem eventSystem) {
        this.raycaster = raycaster;
        this.eventSystem = eventSystem;
    }

    public Touch [ ] HandlInput ( ) {
        foreach (Touch touch in Input.touches) {
            PointerEventData pointerEventData = new PointerEventData (eventSystem);
            pointerEventData.position = touch.position;

            List<RaycastResult> results = new List<RaycastResult> ( );

            raycaster.Raycast (pointerEventData, results);

            foreach (RaycastResult result in results) {
                switch (result.gameObject.name) {
                    case "LeftBtn":
                        btnStates [(int) ENotePos.L] = touch;
                        break;
                    case "MidBtn":
                        btnStates [(int) ENotePos.M] = touch;
                        break;
                    case "RightBtn":
                        btnStates [(int) ENotePos.R] = touch;
                        break;
                }
            }
        }
        return btnStates;
    }
}
