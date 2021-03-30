using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float health = 100;
    private int money = 0;
    private float next = 4;
    private CamShake cams;
    public Text HealthText;
    public Text MoneyText;
    public Text NewMoney;
    public FPSController fps;
    public AudioSource a;
    public AudioClip PlayerHit;
    public AudioClip YouDied;
    public bool dead = false;
    public Image bloodImage;
    public GameObject youLose;
    public Image youLoseBackground;

    private void Start()
    {
        cams = GetComponentInChildren<CamShake>();   
    }

    private void Update()
    {
        if (bloodImage.color.a > 0 )
        {
            if(Time.time > next)
            {
                next = Time.time + 2;
                Color Transparent = new Color(1, 1, 1, 0);
                bloodImage.color = Color.Lerp(bloodImage.color, Transparent, 20 * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(dead == false)
        {
            new WaitForSeconds(0.5f);
            a.PlayOneShot(PlayerHit, 0.3f);
            StartCoroutine(cams.Shake(0.2f, 0.2f));
            Color Opaque = new Color(1, 1, 1, 1);
            bloodImage.color = Color.Lerp(bloodImage.color, Opaque, 100 * Time.deltaTime);
        }

        HealthText.text = "Health: " + health.ToString() + "%";

        if (health <= 0)
        {
            health = 20;
            dead = true;
            fps.enabled = false;
            Quaternion newRotation = Quaternion.AngleAxis(45, Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, .5f);
            Invoke("YouLose", 5);
            Invoke("ChangeScene", 15);
        }
    }

    private void YouLose()
    {
        youLose.SetActive(true);
        youLoseBackground.gameObject.SetActive(true);
        if (!a.isPlaying)
        {
            a.PlayOneShot(YouDied, 0.5f);
        }
    }

    private void ChangeScene()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(2);
    }

    public void AddMoney(int addMoney)
    {
        money += addMoney;
        NewMoney.color = Color.green;
        NewMoney.text = "+" + addMoney.ToString() + "$";
        Invoke(nameof(Temp), 1);
    }

    private void Temp()
    {
        MoneyText.text = "Money: " + money.ToString() + "$";
        NewMoney.text = "";
    }

    public void RemoveMoney(int removeMoney)
    {
        money -= removeMoney;
        NewMoney.color = Color.red;
        NewMoney.text = "-" + removeMoney.ToString() + "$";
        Invoke(nameof(Temp), 1);
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if(health > 100)
        {
            health = 100;
        }
        HealthText.text = "Health: " + health.ToString() + "%";
    }
}
