﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public bool isCursorLocked = false;

    // Use this for initialization
    void Start () {
        ToggleCursorState();
    }

    // Update is called once per frame
    void Update () {
        CheckForInput();
        CheckIfCursorShouldBeLocked();
    }

    void ToggleCursorState() {
        isCursorLocked = !(isCursorLocked);
    }

    void CheckForInput() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleCursorState();
        }
    }

    void CheckIfCursorShouldBeLocked() {
        if (isCursorLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
