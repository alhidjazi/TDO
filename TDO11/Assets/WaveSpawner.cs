using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public GameManager gameManager;
    //[SerializeField]
    //private Transform enemyPrefab;
    public Wave[] waves;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves = 5f;

    private float countdown = 5f;

    [SerializeField]
    private Text waveCountdownTimer;

    private int waveIndex = 0;

    //vie de l'ennemi ici commmence par 0
    public void Start()
    {
        EnemiesAlive = 0;
    }
    // si la vie de l'ennemi est gros a 0
    public void Update()
    {
        if (EnemiesAlive > 0) 
        {
            return;
        }
        //si num de vague egal a longeur de vague alors on appele la gameManager.WinLevel() et on ferme le nivaeu actuel
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        // num de retur si est min et egal a 0 alors on appele la fonct Vague d'apparition qui lieu avec coroutine

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }


        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownTimer.text = string.Format("{0:00.00}", countdown);
    }
    IEnumerator SpawnWave()
    {
        

        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];

        //pour dire que les nombres totals de l'ennemi est tel...
        EnemiesAlive = wave.count;


        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f /wave.rate);
        }

        waveIndex++;
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

       
    }
}

