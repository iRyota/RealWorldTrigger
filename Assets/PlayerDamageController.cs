using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageController : MonoBehaviour {

    private Image img;

    // Use this for initialization
    void Start () {
        img = GetComponent<Image>();
        img.color = Color.clear;
    }

    // Update is called once per frame
    void Update () {
        // if (Input.GetMouseButtonDown(0)) {
        //     img.color = new Color(0.5f, 0f, 0f, 0.5f);
        // } else {
        //     img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
        // }
        img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
    }

    public void PlayEffect() {
        img.color = new Color(0.5f, 0f, 0f, 0.5f);
    }
}
