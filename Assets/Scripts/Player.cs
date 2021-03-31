using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int money = 0;
    private float passiveMoney;
    private float MoneyPer30Seconds;
    private float Countdown = 120;
    private string CountdownText;
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
    public static float multy;
    private float MaxHealth;
    private float currentHealth;
    public WaveSystem wave;
    private bool done;

    private void Start()
    {
        cams = GetComponentInChildren<CamShake>();
        passiveMoney = 1000 * multy;
        MoneyPer30Seconds = passiveMoney / 4;
        MaxHealth = 100 * multy;
        currentHealth = MaxHealth;
        HealthText.text = "Health: " + ((currentHealth/MaxHealth) * 100).ToString() + "%";
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
        if (wave.startCountdown)
        {
            if (!(Countdown < 0))
            {
                Countdown -= Time.deltaTime;
                CountdownText = Countdown.ToString("F0");
                if (CountdownText.Equals("90") || CountdownText.Equals("60") || CountdownText.Equals("30") || CountdownText.Equals("0"))
                {
                    if (!done)
                    {
                        AddMoney((int)MoneyPer30Seconds);
                        passiveMoney -= MoneyPer30Seconds;
                        done = true;
                        Invoke("NotDone", 1);
                    }
                }
            }
            else
            {
                wave.startCountdown = false;
            }
        }
        if (wave.EndWave)
        {
            wave.startCountdown = false;
            wave.EndWave = false;
            done = true;
            AddMoney((int)passiveMoney);
            passiveMoney = MoneyPer30Seconds * 4;
            Countdown = 120;
            Invoke("NotDone", 1);
        }

    }

    private void NotDone()
    {
        done = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(dead == false)
        {
            new WaitForSeconds(0.5f);
            a.PlayOneShot(PlayerHit, 0.3f);
            StartCoroutine(cams.Shake(0.2f, 0.2f));
            Color Opaque = new Color(1, 1, 1, 1);
            bloodImage.color = Color.Lerp(bloodImage.color, Opaque, 100 * Time.deltaTime);
        }

        HealthText.text = "Health: " + ((currentHealth / MaxHealth) * 100).ToString() + "%";

        if (currentHealth <= 0)
        {
            currentHealth = 20;
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
        currentHealth += amount * multy;
        if(currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        HealthText.text = "Health: " + ((currentHealth / MaxHealth) * 100).ToString() + "%";
    }
}
