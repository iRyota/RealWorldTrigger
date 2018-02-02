using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

    private Image img;
    public bool enableFadeIn = false;
    public bool enableFadeOut = false;
    private bool startFadeOut = false;
    private Color black;

    // Use this for initialization
    void Start () {
        img = GetComponent<Image>();
        black = new Color(0f, 0f, 0f, 2f);
        if (!enableFadeIn) {
            img.color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update () {
        if (!startFadeOut) {
            img.color = Color.Lerp(img.color, Color.clear, 0.5f*Time.deltaTime);
        } else {
            img.color = Color.Lerp(img.color, black, 0.5f*Time.deltaTime);
        }
    }

    public void ToggleStatus() {
        if (enableFadeOut) {
            startFadeOut = true;
        }
    }

}
