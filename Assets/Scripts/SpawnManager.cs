using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField] private GameObject enemyPref;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject[] powerUpsPref;

    private bool stopSpawning = false;

    public void StartSpawn()
    {

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUps());
    }

    void Update()
    {
         
    }

    IEnumerator SpawnEnemyRoutine()
    {
        //spawn için biraz bekle
        yield return new WaitForSeconds(1.9f);
        //while loop  sonsuz defa döner true
        while (stopSpawning == false)
        {
            //Instantiae enemypref with random x pos
            Vector3 randomPos = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0);
            GameObject newEnemy = Instantiate(enemyPref, randomPos, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform; // 
            // yield every 3 sec
            yield return new WaitForSeconds(1f);
        }
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }

    IEnumerator SpawnPowerUps()
    {
        yield return new WaitForSeconds(2f);

        while (stopSpawning == false)
        {
            //Instantiae enemypref with random x pos
            Vector3 randomPos = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0);
            Instantiate(powerUpsPref[Random.Range(0, powerUpsPref.Length)], randomPos, Quaternion.identity);
            // yield every 5-7 sec
            float waitTime = Random.Range(3, 5);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
