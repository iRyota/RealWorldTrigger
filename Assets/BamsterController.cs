using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsterController : MonoBehaviour {
    public enum EnemyState {
        Wait,
        Chase,
        CloseAttack,
        DistantAttack,
        Damage,
        Freeze,
        Dead
    };

    [SerializeField]
    private float freezeAfterAttack = 5f;

    public float speed = 1f;
    public float rotationSmooth = 1f;

    private Transform player;

    public EnemyState state = EnemyState.Wait;

    private Animator animator;

    private float elapsedTime = 0f;

    public Transform muzzle;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        Vector3 target = player.position;
        target.y++;
        muzzle.LookAt(target);
        int HP = animator.GetInteger("HP");
        Debug.Log(HP);
        if (HP < 1) {
            SetState("Dead");
            GameObject.FindWithTag("GameEndText").GetComponent<TextFadeController>().ToggleStatus();
        } else {
            if (state == EnemyState.Chase) {
                animator.SetBool("isWalking", true);
                // 方向転換
                Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

                // 前進
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            } else if (state == EnemyState.Wait) {
                animator.SetBool("isWalking", false);
                elapsedTime += Time.deltaTime;

                if (elapsedTime > 3f) {
                    SetState("Chase");
                }
            } else if (state == EnemyState.DistantAttack) {
                animator.SetBool("isAttacking", true);
            } else if (state == EnemyState.Freeze) {
                animator.SetBool("isAttacking", false);
                elapsedTime += Time.deltaTime;

                if (elapsedTime > freezeAfterAttack) {
                    SetState("Wait");
                }
            }
        }
    }

    public EnemyState GetState() {
        return state;
    }

    public void SetState(string mode) {
        if (mode == "DistantAttack") {
            elapsedTime = 0;
            state = EnemyState.DistantAttack;
            Debug.Log("DistantAttack");
        } else if (mode == "Wait") {
            elapsedTime = 0;
            state = EnemyState.Wait;
            Debug.Log("Wait");
        } else if (mode == "Chase") {
            elapsedTime = 0;
            state = EnemyState.Chase;
            Debug.Log("Chase");
        } else if (mode == "Damage") {
            state = EnemyState.Damage;
            Debug.Log("Damage");
        } else if (mode == "CloseAttack") {
            state = EnemyState.CloseAttack;
        } else if (mode == "Freeze") {
            elapsedTime = 0;
            state = EnemyState.Freeze;
            Debug.Log("Freeze");
        } else if (mode == "Dead") {
            elapsedTime = 0;
            state = EnemyState.Dead;
            Debug.Log("Dead");
        }
    }
}
