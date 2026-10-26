using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Gun : MonoBehaviour
{
   private CharacterInput controls;

    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    [SerializeField] private PlayerGunSelect GunSelect;


    private void Awake()
    {
        controls = new CharacterInput();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Player.Shoot.triggered)
        {
            //Shoot();
        }
        
    }
    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }
    /*void Shoot()
    {
        RaycastHit hit;
        if (Mouse.current.leftButton.isPressed && GunSelect.ActiveGun != null)
        {
            GunSelect.ActiveGun.shoot();
            Debug.Log("shooting");
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.green, 1f);
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Target enemy = hit.transform.GetComponent<Target>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

            }
            else
            {
                //Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.green, 1f);
            }
           // Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.green, 1f);
        }
    }*/
}
