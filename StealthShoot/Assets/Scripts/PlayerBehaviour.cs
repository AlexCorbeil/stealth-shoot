using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float maxSpeed;
    public Rigidbody rb;
    public List<WeaponBehaviour> weapons = new List<WeaponBehaviour>();
    public int selectedWeaponIndex;

    void Awake() {
        References.thePlayer = gameObject;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        selectedWeaponIndex = 0;
    }

    void Update() {
        Movement();
        Shoot();
        ChangeWeapon();  
    }

    void Movement() {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = inputVector * maxSpeed;        
        Vector3 lookAtPos = CursorAim();
        transform.LookAt(lookAtPos);
    }

    void Shoot() {        
        if(weapons.Count > 0 && Input.GetButton("Fire1")){
            weapons[selectedWeaponIndex].FireBullet(CursorAim());
        }
    }

    void ChangeWeapon() {
        if (weapons.Count > 0) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                selectedWeaponIndex++;
                if (selectedWeaponIndex >= weapons.Count) {
                    selectedWeaponIndex = 0;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                selectedWeaponIndex--;
                if (selectedWeaponIndex < 0) {
                    selectedWeaponIndex = weapons.Count - 1;
                }
            }

            for (int i = 0; i < weapons.Count; i++) {
                if (i == selectedWeaponIndex) {
                    weapons[i].gameObject.SetActive(true);
                }
                else {
                    weapons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    Vector3 CursorAim() {
        Ray camToCursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(camToCursorRay, out float rayDist);
        Vector3 cursorPos = camToCursorRay.GetPoint(rayDist);

        return cursorPos;
    }

    void OnTriggerEnter(Collider collider) {
        GameObject myWeapon = collider.gameObject;
        WeaponBehaviour myWeaponBehaviour = myWeapon.GetComponent<WeaponBehaviour>();
        if(myWeaponBehaviour != null) {
            GameObject newWeapon = Instantiate(myWeapon, transform.position, transform.rotation, transform);
            weapons.Add(newWeapon.GetComponent<WeaponBehaviour>());
            selectedWeaponIndex = weapons.Count - 1;
            Destroy(collider.gameObject);   
        }
    }
}