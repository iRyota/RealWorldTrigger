using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour {

    private float elapsedTime = 0f;
    private float waitForAttack = 3f;

    void OnTriggerStay(Collider col) {
        if (col.tag == "Player") {
            BamsterController.EnemyState state = GetComponentInParent<BamsterController>().GetState();

            if (state == BamsterController.EnemyState.Wait && state != BamsterController.EnemyState.Dead) {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > waitForAttack) {
                    GetComponentInParent<BamsterController>().SetState("DistantAttack");
                    elapsedTime = 0;
                }
            } else if (state == BamsterController.EnemyState.Chase && state != BamsterController.EnemyState.Dead) {
                GetComponentInParent<BamsterController>().SetState("Wait");
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            BamsterController.EnemyState state = GetComponentInParent<BamsterController>().GetState();

            if ((state == BamsterController.EnemyState.Wait || state == BamsterController.EnemyState.Freeze) && state != BamsterController.EnemyState.Dead) {
                GetComponentInParent<BamsterController>().SetState("Chase");
            }
        }
    }
}
