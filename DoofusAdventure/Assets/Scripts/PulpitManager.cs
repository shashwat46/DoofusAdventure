using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public float minPulpitDestroyTime = 4f;
    public float maxPulpitDestroyTime = 5f;
    public float pulpitSpawnTime = 2.5f;
    public float platformSize = 9f;
    public float initialHeight = 100f; // New variable for initial height

    private List<GameObject> activePulpits = new List<GameObject>();
    private Vector3 lastPulpitPosition;
    private DoofusController doofusController;

   public void StartGame()
    {
        doofusController = FindObjectOfType<DoofusController>();
        if (doofusController == null)
        {
            Debug.LogError("DoofusController not found in the scene!");
        }

        // Spawn initial pulpit at the specified height
        ClearAllPulpits();
        SpawnInitialPulpit();
        StartCoroutine(PulpitSpawnRoutine());
    }

    private void ClearAllPulpits()
    {
        foreach (GameObject pulpit in activePulpits)
        {
            Destroy(pulpit);
        }
        activePulpits.Clear();
    }

    private void SpawnInitialPulpit()
    {
        Vector3 initialPosition = new Vector3(0, initialHeight, 0);
        SpawnPulpit(initialPosition);
    }

    private IEnumerator PulpitSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(pulpitSpawnTime);
            if (activePulpits.Count < 2)
            {
                SpawnAdjacentPulpit();
            }
        }
    }

    private void SpawnAdjacentPulpit()
    {
        Vector3[] possiblePositions = new Vector3[]
        {
            lastPulpitPosition + new Vector3(platformSize, 0, 0),
            lastPulpitPosition + new Vector3(-platformSize, 0, 0),
            lastPulpitPosition + new Vector3(0, 0, platformSize),
            lastPulpitPosition + new Vector3(0, 0, -platformSize)
        };

        List<Vector3> validPositions = new List<Vector3>();
        foreach (Vector3 pos in possiblePositions)
        {
            if (!IsPulpitAtPosition(pos))
            {
                validPositions.Add(pos);
            }
        }

        if (validPositions.Count > 0)
        {
            int randomIndex = Random.Range(0, validPositions.Count);
            SpawnPulpit(validPositions[randomIndex]);
        }
        else
        {
            Debug.LogWarning("No valid positions to spawn new pulpit!");
        }
    }

    private void SpawnPulpit(Vector3 position)
    {
        if (pulpitPrefab == null)
        {
           
            return;
        }

        GameObject pulpit = Instantiate(pulpitPrefab, position, Quaternion.identity);
        if (pulpit == null)
        {
          
            return;
        }

        activePulpits.Add(pulpit);
        lastPulpitPosition = position;

        float destroyTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
        
        PulpitTimer timer = pulpit.GetComponent<PulpitTimer>();
        if (timer == null)
        {
            timer = pulpit.AddComponent<PulpitTimer>();
            if (timer == null)
            {
                Debug.LogError("Failed to add PulpitTimer component!");
                return;
            }
        }
        timer.Initialize(destroyTime);

        StartCoroutine(RemovePulpitAfterTime(pulpit, destroyTime));

        if (doofusController != null)
        {
            doofusController.OnNewPulpitReached();
        }
        else
        {
            Debug.LogWarning("DoofusController is null when trying to notify of new pulpit!");
        }
    }

    private IEnumerator RemovePulpitAfterTime(GameObject pulpit, float time)
    {
        yield return new WaitForSeconds(time);
        if (pulpit != null)
        {
            activePulpits.Remove(pulpit);
            Destroy(pulpit);

            // Update the score when a pulpit is destroyed
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.UpdateScore();
            }
            else
            {
                Debug.LogWarning("ScoreManager instance is null!");
            }

            // If this was the last pulpit, spawn a new one
            if (activePulpits.Count == 0)
            {
                SpawnAdjacentPulpit();
            }
        }
    }

    private bool IsPulpitAtPosition(Vector3 position)
    {
        foreach (GameObject pulpit in activePulpits)
        {
            if (pulpit.transform.position == position)
            {
                return true;
            }
        }
        return false;
    }
}