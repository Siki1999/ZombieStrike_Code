using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glock : MonoBehaviour
{
    public float damage;
    public float HeadshotDamage;
    private float range = 100;
    private float FireRate = 15;
    private float nextTimeToFire = 0;
    private int bulletsLeft = 20;
    private int magazineSize = 20;
    private int reloadTime = 2;
    private bool reloading;
    private Vector3 upRecoil = new Vector3(-2, 1, 0);
    private Vector3 originalRotation;
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

    void Start()
    {
        ammo.text = bulletsLeft + " / ∞";
        originalRotation = transform.localEulerAngles;
        a = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        reloading = false;
        anim.SetBool("Reloading", false);
        ammo.text = bulletsLeft + " / ∞";
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

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

            if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire && !reloading && bulletsLeft > 0)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }
    }

    private void Reload()
    {
        reloading = true;
        anim.SetBool("Reloading", true);
        a.PlayOneShot(aReload, 1.5f);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        if (reloading)
        {
            bulletsLeft = magazineSize;
            reloading = false;
            ammo.text = bulletsLeft + " / ∞";
            anim.SetBool("Reloading", false);
        }
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
        MuzzleFlash.Play();
        a.PlayOneShot(aShot, 0.5f);
        AddRecoil();
        StartCoroutine(camShake.Shake(0.05f, 0.05f));
        bulletsLeft--;
        ammo.text = bulletsLeft + " / ∞";

        RaycastHit hit;
        if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, range))
        {
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

    public void AddDamage(int damage, int HeadShotDamage)
    {
        this.damage += damage;
        HeadshotDamage += HeadShotDamage;
    }
}
