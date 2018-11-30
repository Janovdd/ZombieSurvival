using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ZombieSpawner : MonoBehaviour {
    public enum SpawnState { Spawning, Waiting, Building, Counting };

    [System.Serializable]
    public class Wave
    {
        public Transform enemy;
        public int count;
        public float rate;
    }

    public GameObject waveCamera;
    public GameObject buildCamera;

    public GameObject waveUI;
    public GameObject buildUI;

    public GameObject player;
    public GameObject plane;

    public Wave[] waves;
    private int nextWave = 0;
    private int currentWave = 1;

    public TMP_Text waveText;
    public TMP_Text enemiesText;
    public TMP_Text countDownText;

    public GameObject countDownObject;


    public Transform[] spawnPoints;


    public float timeBetweenWaves = 3f;
    public float waveCountdown;

    private float searchCountdown = 1f;
    private int enemiesAlive = 0;

    private SpawnState state = SpawnState.Counting;

	// Use this for initialization
	void Start () {
        waveCountdown = timeBetweenWaves;

        waveCamera.SetActive(true);
        buildCamera.SetActive(false);
        waveUI.SetActive(true);
        buildUI.SetActive(false);
        player.SetActive(true);
        plane.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        waveText.text = "WAVE " + currentWave;
        enemiesText.text = "enemies alive: " + enemiesAlive;
        countDownText.text = waveCountdown.ToString("F0");
        countDownObject.SetActive(false);



        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                BuildMode();
            }
            else
            {
                return;
            }
        }

        if (state == SpawnState.Counting)
        {
            if (waveCountdown <= 0)
            {
                if (state != SpawnState.Spawning)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                countDownObject.SetActive(true);
                waveCountdown -= Time.deltaTime;
            }
        }
       
    }

    void BuildMode()
    {
        state = SpawnState.Building;

        waveCamera.SetActive(false);
        buildCamera.SetActive(true);
        waveUI.SetActive(false);
        buildUI.SetActive(true);
        player.SetActive(false );
        plane.SetActive(true);

    }

    public void WaveCompleted()
    {
        state = SpawnState.Counting;

        waveCamera.SetActive(true);
        buildCamera.SetActive(false);
        waveUI.SetActive(true);
        buildUI.SetActive(false);
        player.SetActive(true);
        plane.SetActive(false);

        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            currentWave = 1;
        }
        else
        {
            nextWave++;
            currentWave++;

        }


    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }


    IEnumerator SpawnWave (Wave wave)
    {
        state = SpawnState.Spawning;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.count);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, sp.position, sp.rotation);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Test");
    }
}
