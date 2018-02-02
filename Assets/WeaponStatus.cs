using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Asteroid,
    Meteora
};

public class WeaponStatus : MonoBehaviour {

    public float power = 1f;
    public float range = 10f;
    public int maxCharge = 10;
    public WeaponType weaponType = WeaponType.Asteroid;

    [SerializeField] private AudioSource shotSE; // sound effect
    [SerializeField] private Light shotLight;
    [SerializeField] private GameObject shotEffect; // visual effect when shot
    [SerializeField] private GameObject asteroidEffect;  // visual effect when hit
    [SerializeField] private GameObject meteoraEffect;  // visual effect when hit

    public AudioSource GetShotSE() {
        return shotSE;
    }

    public Light GetShotLight() {
        return shotLight;
    }

    public GameObject GetShotEffect() {
        return shotEffect;
    }

    public GameObject GetHitEffect() {
        if (weaponType == WeaponType.Asteroid) {
            return asteroidEffect;
        } else if (weaponType == WeaponType.Meteora) {
            return meteoraEffect;
        } else {
            return null;
        }
    }

    public float GetPower() {
        return power;
    }

    public float GetRange() {
        return range;
    }

    public int GetMaxCharge() {
        return maxCharge;
    }

    public WeaponType GetWeaponType() {
        return weaponType;
    }

    public void SetWeaponType(WeaponType type) {
        weaponType = type;
    }
}
