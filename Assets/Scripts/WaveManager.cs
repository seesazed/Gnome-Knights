using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public int zombiesPerWave = 10;
    public float spawnDelay = 0.5f;

    private bool waveInProgress = false;
    private List<GameObject> activeZombies = new List<GameObject>();

    // Wave settings
    public int currentWave = 1;
    public float waveMultiplier = 1.1f; // How much to increase health per wave

    public bool IsWaveInProgress()
    {
        return waveInProgress;
    }

    public bool IsWaveCleared()
    {
        // Remove nulls from the list in case zombies were destroyed
        activeZombies.RemoveAll(z => z == null);
        return activeZombies.Count == 0;
    }

    public void StartNextWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        waveInProgress = true;

        // Calculate the new zombie health multiplier based on wave number
        float healthMultiplier = Mathf.Pow(waveMultiplier, currentWave - 1); // Exponential increase each wave

        // Spawn zombies
        for (int i = 0; i < zombiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
            ZombieHealth zombieHealth = zombie.GetComponent<ZombieHealth>();

            // Apply the health multiplier to each zombie
            if (zombieHealth != null)
            {
                zombieHealth.SetWaveMultiplier(healthMultiplier);
            }

            activeZombies.Add(zombie);
            yield return new WaitForSeconds(spawnDelay);
        }

        // Wait until all zombies are dead
        yield return new WaitUntil(() => IsWaveCleared());

        waveInProgress = false;

        // Increase the wave number for the next wave
        currentWave++;
    }
}
