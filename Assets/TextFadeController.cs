using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextFadeController : MonoBehaviour {
    private Text text;
    private Color green;
    private bool GameEnd = false;
    private float elapsedTime = 0f;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        text.color = Color.clear;
        green = new Color(0f,1f,0.6f,1f);
    }

    // Update is called once per frame
    void Update () {
        if (GameEnd) {
            text.color = Color.Lerp(text.color, green, Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime > 10f) {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void ToggleStatus() {
        GameEnd = true;
    }
}
