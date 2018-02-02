using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrionCubeGenerator : MonoBehaviour {
    public GameObject trionPrefab;

    // Update is called once per frame
    void Update () {
        keyboard();
    }

    void keyboard() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject trion = Instantiate(trionPrefab) as GameObject;
            trion.transform.position = Camera.main.transform.position;
            // trion.transform.position.z += 1;
            trion.GetComponent<TrionCubeController>().Shoot(Camera.main.transform.forward * 3000f);
        }
    }
}
