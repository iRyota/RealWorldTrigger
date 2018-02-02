﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticle : MonoBehaviour {
    private ParticleSystem particle;

    // Use this for initialization
    void Start () {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {
        if (particle.isStopped) {
            Destroy(this.gameObject);
        }
    }
}
