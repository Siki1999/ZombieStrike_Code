using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    public enum spawnState { SPAWNING, WAITING, COUNTING };
    public Transform ai;
    public Text textWaveName;
    public Text textCountdown;
    public Text tip;
    public GameObject youWin;
    public AudioSource a;
    public AudioClip aYouWin;
    public AudioClip aRoundStart;
    public AudioClip aCountdown;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int enemyCount;
        public float delay;
    }

    public static Wave[] waves;
    private int nextWave = 0;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 15;
    public float waveCountdown;
    private float searchCountdown = 1;
    public spawnState state = spawnState.COUNTING;
    public bool startCountdown;
    public bool EndWave;

    private void Start()
    {
        waveCountdown = timeBetweenWaves + 15;
        int counter = 1;
        ai.GetComponent<EnemyAI>().health = 100;
        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i] == null)
            {
                Wave w = new Wave();
                w.name = "Wave " + (i+1);
                w.enemy = ai;
                if (waves.Length == 3)
                {
                    w.enemyCount = counter * 20;
                }
                else if (waves.Length == 5)
                {
                    w.enemyCount = counter * 12;
                }
                else if (waves.Length == 7)
                {
                    w.enemyCount = (-4) + (counter * 10);
                }
                else
                {
                    w.enemyCount = 6 + (counter * 4);
                }
                w.delay = 1.5f;
                waves[i] = w;
                counter++;
            }
        }
        textWaveName.text = "Match starts in:";
        Invoke("HideTip", 15);
    }

    private void Update()
    {
        if(state == spawnState.WAITING)
        {
            if (!EnemyAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != spawnState.SPAWNING)
            {
                textCountdown.text = "";
                a.PlayOneShot(aRoundStart, 0.2f);
                StartCoroutine( SpawnWave( waves[nextWave] ) );
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            textCountdown.text = waveCountdown.ToString("F0");
            if(textCountdown.text.Equals("3") && !a.isPlaying)
            {
                PlayCountdown();
            }
        }
    }

    private void PlayCountdown()
    {
        a.PlayOneShot(aCountdown, 0.2f);
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        startCountdown = true;
        state = spawnState.SPAWNING;
        textWaveName.text = _wave.name;

        for(int i = 0; i < _wave.enemyCount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(_wave.delay);
        }

        state = spawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _SP = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _SP.position, _SP.rotation);
    }

    bool EnemyAlive()
    {
        searchCountdown -= Time.deltaTime;

        if(searchCountdown <= 0)
        {
            searchCountdown = 1;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    void WaveCompleted()
    {
        state = spawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextWave + 1 > waves.Length - 1)
        {
            youWin.SetActive(true);
            a.PlayOneShot(aYouWin, 0.5f);
            Invoke("ChangeScene", 15);
        }
        else
        {
            nextWave++;
            textCountdown.enabled = true;
            Invoke("EndWavePassiveMoney", 1);
            if (waves.Length == 3)
            {
                PlayerManager.instance.player.GetComponent<Player>().AddMoney(2500);
                ai.GetComponent<EnemyAI>().health += 200;
            }
            else if (waves.Length == 5)
            {
                PlayerManager.instance.player.GetComponent<Player>().AddMoney(1500);
                ai.GetComponent<EnemyAI>().health += 100;
            }
            else if (waves.Length == 7)
            {
                PlayerManager.instance.player.GetComponent<Player>().AddMoney(1000);
                ai.GetComponent<EnemyAI>().health += 70;
            }
            else
            {
                PlayerManager.instance.player.GetComponent<Player>().AddMoney(1000);
                ai.GetComponent<EnemyAI>().health += 20;
            }
        }
    }

    void EndWavePassiveMoney()
    {
        EndWave = true;
    }

    void HideTip()
    {
        tip.enabled = false;
    }

    private void ChangeScene()
    {
        AudioListener.pause = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(2);
    }
}
