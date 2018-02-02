using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {
    private float shotRange;
    private WeaponStatus weaponStatus;
    public GameObject muzzle;
    private float muzzleRadius;
    private GameObject nearestEnemy;
    public Animator bamsterAnimator;

    // Use this for initialization
    void Start () {
        weaponStatus = GameObject.FindWithTag("Equip").transform.GetChild(0).GetComponent<WeaponStatus>();
        shotRange = weaponStatus.GetRange();
        muzzleRadius = muzzle.transform.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update () {
        Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + muzzle.transform.forward * shotRange);
    }

    public void Judge() {
        ShotEffect();

        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);

        RaycastHit[] hits;
        RaycastHit effectHit;

        // hits = Physics.SphereCastAll(ray, muzzleRadius, shotRange, LayerMask.GetMask("Enemy"));
        if (Physics.SphereCast(ray, muzzleRadius, out effectHit, shotRange)) {
            GameObject.Instantiate(weaponStatus.GetHitEffect(), effectHit.point, Quaternion.identity);
        }

        float distance = shotRange;

        nearestEnemy = null;

        // if (weaponStatus.GetWeaponType() == WeaponType.HandGun) {
        //     foreach(RaycastHit hit in hits) {
        //         if (Vector3.Distance(transform.position, hit.point) < distance) {
        //             distance = Vector3.Distance(transform.position, hit.point);
        //             nearestEnemy = hit.collider.gameObject;
        //         }
        //     }
        //     if (nearestEnemy != null) {
        //         Destroy(nearestEnemy);
        //     }
        // } else {
        //     foreach(RaycastHit hit in hits) {
        //         Destroy(hit.collider.gameObject);
        //     }
        // }

        hits = Physics.SphereCastAll(ray, muzzleRadius, shotRange, LayerMask.GetMask("Bamster"));
        if (weaponStatus.GetWeaponType() == WeaponType.Asteroid) {
            foreach(RaycastHit hit in hits) {
                if (Vector3.Distance(transform.position, hit.point) < distance) {
                    distance = Vector3.Distance(transform.position, hit.point);
                    nearestEnemy = hit.collider.gameObject;
                }
            }
            if (nearestEnemy != null) {
                int HP = bamsterAnimator.GetInteger("HP");
                HP--;
                GameObject.FindWithTag("Player").GetComponent<YumaController>().UseTrion(5);
                if (HP < 0) {
                    HP = 0;
                }
                bamsterAnimator.SetInteger("HP", HP);
            }
        } else {
            foreach(RaycastHit hit in hits) {
                int HP = bamsterAnimator.GetInteger("HP");
                HP-=5;
                GameObject.FindWithTag("Player").GetComponent<YumaController>().UseTrion(20);
                if (HP < 0) {
                    HP = 0;
                }
                bamsterAnimator.SetInteger("HP", HP);
            }
        }
    }

    private void ShotEffect() {
        weaponStatus.GetShotSE().Stop();
        weaponStatus.GetShotSE().Play();

        // weaponStatus.GetShotEffect().GetComponent<ParticleSystem>().Stop();
        // weaponStatus.GetShotEffect().GetComponent<ParticleSystem>().Play();

        weaponStatus.GetShotLight().enabled = false;
        weaponStatus.GetShotLight().enabled = true;

        StartCoroutine(DisableLight());
    }

    IEnumerator DisableLight() {
        yield return new WaitForSeconds(0.2f);
        weaponStatus.GetShotLight().enabled = false;
    }

    public void SwitchWeapon() {
        WeaponType type = weaponStatus.GetWeaponType();
        if (type == WeaponType.Asteroid) {
            weaponStatus.SetWeaponType(WeaponType.Meteora);
        } else if (type == WeaponType.Meteora) {
            weaponStatus.SetWeaponType(WeaponType.Asteroid);
        }
    }
}
