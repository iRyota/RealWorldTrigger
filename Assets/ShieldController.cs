using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {

    public GameObject shieldPrefab;
    public Transform shieldMuzzle;

    public GameObject GenerateShield() {
        GameObject shield = Instantiate(shieldPrefab) as GameObject;
        shield.transform.position = shieldMuzzle.position;
        shield.transform.rotation = shieldMuzzle.rotation;
        return shield;
    }

}
