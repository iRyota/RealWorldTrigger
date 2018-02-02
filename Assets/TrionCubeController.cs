using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrionCubeController : MonoBehaviour {
    public void Shoot(Vector3 dir) {
        GetComponent<Rigidbody>().AddForce(dir);
    }
	// Use this for initialization
	void Start () {
        // Shoot(new Vector3(0, 200, 2000));
	}

    void OnCollisionEnter(Collision other) {
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }

	// // Update is called once per frame
	// void Update () {

	// }
}
