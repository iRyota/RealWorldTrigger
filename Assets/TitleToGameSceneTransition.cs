using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleToGameSceneTransition : MonoBehaviour {
    private FadeController fade;
    private float elapsedTime = 0;
    private bool startClock = false;

    // Use this for initialization
    void Start () {
        fade = GameObject.FindWithTag("Fade").GetComponent<FadeController>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1")) {
            fade.ToggleStatus();
            startClock = true;
        }
        if (startClock) {
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime > 2f) {
            SceneManager.LoadScene("GameScene");
        }
    }
}
