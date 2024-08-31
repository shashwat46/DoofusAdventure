using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
    public string jsonURL = "https://s3.ap-south-1.amazonaws.com/superstars.assetbundles.testbuild/doofus_game/doofus_diary.json";
    public GameData gameData;
    public bool isDataLoaded = false;
    public float downloadTimeout = 10f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(DownloadGameData());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(DownloadGameData());
    }

     IEnumerator DownloadGameData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(jsonURL))
        {
            webRequest.timeout = Mathf.RoundToInt(downloadTimeout);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonText = webRequest.downloadHandler.text;
                gameData = JsonUtility.FromJson<GameData>(jsonText);
                isDataLoaded = true;
                Debug.Log("Game data loaded successfully");
            }
            else
            {
                Debug.LogError("Error downloading game data: " + webRequest.error);
                SetDefaultValues();
            }
            ApplyGameData();
        }
    }

    public void SetDefaultValues()
    {
        gameData = new GameData
        {
            player_data = new PlayerData { speed = 3f },
            pulpit_data = new PulpitData
            {
                min_pulpit_destroy_time = 4f,
                max_pulpit_destroy_time = 5f,
                pulpit_spawn_time = 2.5f
            }
        };
        isDataLoaded = true;
        Debug.Log("Using default game data");
    }

    public void ApplyGameData()
    {
        if (gameData != null)
        {
            DoofusController doofus = FindObjectOfType<DoofusController>();
            if (doofus != null)
            {
                doofus.speed = gameData.player_data.speed;
            }

            PulpitManager pulpitManager = FindObjectOfType<PulpitManager>();
            if (pulpitManager != null)
            {
                pulpitManager.minPulpitDestroyTime = gameData.pulpit_data.min_pulpit_destroy_time;
                pulpitManager.maxPulpitDestroyTime = gameData.pulpit_data.max_pulpit_destroy_time;
                pulpitManager.pulpitSpawnTime = gameData.pulpit_data.pulpit_spawn_time;
            }

            Debug.Log("Game data applied successfully");
        }
        else
        {
            Debug.LogError("Failed to apply game data: gameData is null");
        }
    }

    
}

[System.Serializable]
public class GameData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}