using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Awp : MonoBehaviour
{
    public float damage;
    public float HeadshotDamage;
    private float range = 150;
    private float FireRate = 0.69f;
    private float nextTimeToFire = 0;
    private int bulletsLeft = 10;
    private int magazineSize = 10;
    private int maxAmmo = 30;
    private float reloadTime = 2.7f;
    private bool reloading;
    private bool isScoped = false;
    private Vector3 upRecoil = new Vector3(-2, 1, 0);
    private Vector3 originalRotation;
    private float normalFOV;
    private float normalSPEED;
    public bool LVL1;
    public bool LVL2;
    public bool LVL3;


    public Camera FpsCam;
    public ParticleSystem MuzzleFlash;
    public GameObject ZombieHit;
    public GameObject FloorHit;
    public GameObject WoodHit;
    private GameObject impact;
    public Text ammo;
    public CamShake camShake;
    public Animator anim;
    public AudioSource a;
    public AudioClip aShot;
    public AudioClip aReload;
    public AudioClip aNoAmmo;
    public GameObject ScopeOverlay;
    public GameObject WeaponCamera;
    public Camera MainCamera;
    public FPSController speed;
    public Image crosshair;

    void Start()
    {
        ammo.text = bulletsLeft + " / " + maxAmmo;
        originalRotation = transform.localEulerAngles;
        a = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        reloading = false;
        anim.SetBool("Reloading", false);
        anim.SetBool("Scoped", false);
        anim.SetBool("Shoot", false);
        ammo.text = bulletsLeft + " / " + maxAmmo;
    }

    private void OnDisable()
    {
        anim.Rebind();
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().dead == false)
        {
            if (bulletsLeft == 0 && !reloading && Input.GetMouseButtonDown(0)) a.PlayOneShot(aNoAmmo, 1.5f);

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && maxAmmo > 0 && !reloading) Reload();

            if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire && !reloading && bulletsLeft > 0)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }

            if (Input.GetMouseButtonDown(1) && !reloading)
            {
                isScoped = !isScoped;
                anim.SetBool("Scoped", isScoped);

                if (isScoped) StartCoroutine(Scoped()); else Unscoped();
            }
        }
    }

    private IEnumerator Scoped()
    {
        yield return new WaitForSeconds(0.15f);
        ScopeOverlay.SetActive(true);
        WeaponCamera.SetActive(false);
        crosshair.enabled = false;

        normalFOV = MainCamera.fieldOfView;
        normalSPEED = speed.lookSpeed;
        MainCamera.fieldOfView = 10f;
        speed.lookSpeed = 0.2f;
    }

    private void Unscoped()
    {
        ScopeOverlay.SetActive(false);
        WeaponCamera.SetActive(true);

        crosshair.enabled = true;
        MainCamera.fieldOfView = normalFOV;
        speed.lookSpeed = normalSPEED;
    }

    private void Reload()
    {
        if (isScoped)
        {
            isScoped = !isScoped;
            anim.SetBool("Scoped", isScoped);
            Unscoped();
        }
        reloading = true;
        anim.SetBool("Reloading", true);
        a.PlayOneShot(aReload, 1.5f);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        if (reloading)
        {
            maxAmmo -= (magazineSize - bulletsLeft);
            if (maxAmmo > 0)
            {
                bulletsLeft = magazineSize;
            }
            else if (bulletsLeft > 0)
            {
                bulletsLeft = maxAmmo + magazineSize;
            }
            else
            {
                bulletsLeft = bulletsLeft + (maxAmmo + magazineSize);
            }
            if (maxAmmo < 0)
            {
                maxAmmo = 0;
            }
            ammo.text = bulletsLeft + " / " + maxAmmo;
            anim.SetBool("Reloading", false);
            Invoke("ReloadFalse", 0.2f);
        }
    }

    void ReloadFalse()
    {
        reloading = false;
    }

    private void AddRecoil()
    {
        transform.localEulerAngles += upRecoil;
        Invoke("StopRecoil", 0.1f);
    }

    private void StopRecoil()
    {
        transform.localEulerAngles = originalRotation;
    }

    void Shoot()
    {
        if (!isScoped)
        {
            MuzzleFlash.Play();
            AddRecoil();
            StartCoroutine(camShake.Shake(0.05f, 0.05f));
        }
        a.PlayOneShot(aShot, 0.5f);
        anim.SetBool("Shoot", true);
        Invoke("BetweenShots", 1);
        bulletsLeft--;
        ammo.text = bulletsLeft + " / " + maxAmmo;
        if (isScoped)
        {
            isScoped = !isScoped;
            anim.SetBool("Scoped", isScoped);
            Unscoped();
        }


        RaycastHit[] hits = Physics.RaycastAll(FpsCam.transform.position, FpsCam.transform.forward, range);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.transform.tag == "Enemy")
            {
                EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
                impact = Instantiate(ZombieHit, hit.point, Quaternion.LookRotation(hit.normal));
                if (hit.collider.tag == "EnemyHead")
                {
                    enemy.TakeDamage(HeadshotDamage);
                }
                else
                {
                    enemy.TakeDamage(damage);
                }
                Destroy(impact, 0.5f);
            }
            if (hit.transform.tag == "Forrest")
            {
                impact = Instantiate(WoodHit, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.5f);
            }
            if (hit.transform.tag == "Terrain")
            {
                impact = Instantiate(FloorHit, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.5f);
            }
        }
    }

    void BetweenShots()
    {
        anim.SetBool("Shoot", false);
    }

    public void AddAmmo()
    {
        maxAmmo += magazineSize;
        ammo.text = bulletsLeft + " / " + maxAmmo;
    }

    public void AddDamage(int damage, int HeadShotDamage)
    {
        this.damage += damage;
        HeadshotDamage += HeadShotDamage;
    }
}
