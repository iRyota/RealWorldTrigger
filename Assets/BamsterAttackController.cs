using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsterAttackController : MonoBehaviour {

    private BamsterController bamster;
    private Animator animator;

    [SerializeField] public GameObject attackEffect;
    [SerializeField] public Transform muzzle;

    // Use this for initialization
    void Start () {
        bamster = GetComponent<BamsterController>();
        animator = GetComponent<Animator>();
    }

    void AttackStart() {
        GameObject.Instantiate(attackEffect, muzzle.position, muzzle.rotation);
    }

    void AttackEnd() {
    }

    // Update is called once per frame
    void StateEnd() {
        bamster.SetState("Freeze");
    }
}
