using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YumaController : MonoBehaviour {
    private CharacterController ctrl;
    private Vector3 vel;
    private Animator animator;
    public float walkSpeed = 1.5f;
    public float runSpeed = 2.5f;
    private bool runFlag = false;

    private Transform myCamera;
    public float cameraRotateLimit;
    // public bool cameraRotForward
    private Quaternion initCameraRot;
    public float cameraSpeed;

    private float xRotate;
    private float yRotate;

    public float mouseSpeed;
    private Quaternion characterRotate;
    private Quaternion cameraRotate;

    private bool characterRotateFlag = false;

    public float jumpPower;
    public float dashJumpPower;

    public Transform spine;
    public Transform cameraPosition;

    private ShotController shot;
    private ShieldController shield;
    private GameObject shieldObject;
    private bool shieldOn = false;

    public int HP;
    public int TP;
    private Slider HPSlider;
    private Slider TPSlider;



    // Use this for initialization
    void Start () {
        ctrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        myCamera = GetComponentInChildren<Camera>().transform;
        initCameraRot = myCamera.localRotation;
        characterRotate = transform.localRotation;
        cameraRotate = myCamera.localRotation;
        shot = GetComponent<ShotController>();
        shield = GetComponent<ShieldController>();
        HPSlider = GameObject.FindWithTag("PlayerHP").GetComponent<Slider>();
        TPSlider = GameObject.FindWithTag("PlayerTP").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update () {
        if (ctrl.isGrounded) {

            RotateCharacter();
            RotateCamera();

            vel = Vector3.zero;
            // if (Input.GetKey(KeyCode.W)) {
            //     vel.z += 1;
            // }
            // if (Input.GetKey(KeyCode.S)) {
            //     vel.z -= 1;
            // }
            // if (Input.GetKey(KeyCode.D)) {
            //     vel.x += 1;
            // }
            // if (Input.GetKey(KeyCode.A)) {
            //     vel.x -= 1;
            // }
            // vel.x = Input.GetAxis("Horizontal");
            // vel.z = Input.GetAxis("Vertical");
            // vel.y = 0;

            vel = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")).normalized;


            float moveSpeed = 0;

            if (Input.GetAxis("Run") != 0) {
                runFlag = true;
                moveSpeed = runSpeed;
            } else {
                runFlag = false;
                moveSpeed = walkSpeed;
            }
            vel *= moveSpeed;

            if (vel.magnitude > 0.001f || characterRotateFlag) {
                if (runFlag && !characterRotateFlag) {
                    animator.SetFloat("Speed", 2.1f);
                } else {
                    animator.SetFloat("Speed", 1.0f);
                }
                // transform.LookAt(transform.position + vel);
            } else {
                animator.SetFloat("Speed", 0);
            }

            if (Input.GetButtonDown("Fire1")) {
                shot.Judge();
                animator.SetBool("Shot",true);
            }

            if (Input.GetButtonDown("Jump")) {
                if (runFlag && vel.magnitude > 0.001f) {
                    vel.y += dashJumpPower;
                } else {
                    vel.y += jumpPower;
                }
            }

            if (Input.GetButtonDown("Switch Weapon")) {
                shot.SwitchWeapon();
            }

            if (Input.GetButtonDown("Shield")) {
                shieldObject = shield.GenerateShield();
                shieldOn = true;
            }
            if (shieldOn == true) {
                shieldObject.transform.position = shield.shieldMuzzle.position;
                shieldObject.transform.rotation = shield.shieldMuzzle.rotation;
                UseTrion(1);
            }
            if (Input.GetButtonUp("Shield")) {
                Destroy(shieldObject);
                shieldOn = false;
            }
        }
        vel.y += Physics.gravity.y * Time.deltaTime;


        ctrl.Move(vel * Time.deltaTime);
    }

    void LateUpdate() {
        RotateBone();

        myCamera.localRotation = cameraPosition.localRotation;
        myCamera.position = cameraPosition.position;
    }

    void RotateCharacter() {
        float yRotate = Input.GetAxis("Mouse X") * mouseSpeed;
        characterRotate *= Quaternion.Euler(0f,yRotate,0f);

        if (yRotate != 0f) {
            characterRotateFlag = true;
        } else {
            characterRotateFlag = false;
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, characterRotate, cameraSpeed * Time.deltaTime);
    }

    void RotateCamera() {
        float xRotate = Input.GetAxis("Mouse Y") * mouseSpeed;

        cameraRotate *= Quaternion.Euler(-1*xRotate,0f,0f);

        if (cameraRotateLimit <= Quaternion.Angle(initCameraRot, cameraRotate)) {
            if (cameraRotateLimit <= cameraRotate.eulerAngles.x && cameraRotate.eulerAngles.x < 90f) {
                cameraRotate = Quaternion.Euler(cameraRotateLimit, cameraRotate.eulerAngles.y, cameraRotate.eulerAngles.z);
            } else if (180f <= cameraRotate.eulerAngles.x && cameraRotate.eulerAngles.x <= 320f) {
                cameraRotate = Quaternion.Euler(360f - cameraRotateLimit, cameraRotate.eulerAngles.y, cameraRotate.eulerAngles.z);
            }
        }

        cameraPosition.localRotation = Quaternion.Slerp(cameraPosition.localRotation, cameraRotate, cameraSpeed * Time.deltaTime);
        // myCamera.localRotation = Quaternion.Slerp(myCamera.localRotation, cameraRotate, cameraSpeed * Time.deltaTime);
    }

    void RotateBone() {
        spine.rotation = Quaternion.Euler(spine.eulerAngles.x, spine.eulerAngles.y, spine.eulerAngles.z + myCamera.localEulerAngles.x);
    }

    public void Damage() {
        HP -= 5;
        HPSlider.value = HP;

        GameObject.FindWithTag("PlayerDamageEffect").GetComponent<PlayerDamageController>().PlayEffect();
    }

    public void UseTrion(int trion) {
        TP -= trion;
        TPSlider.value = TP;
    }

}
