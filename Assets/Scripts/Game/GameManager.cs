using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    public static GameManager Instance { get; private set; }

    // Player State
    [Header("Player Settings")]
    public GameObject playerPrefab;               // Player prefab
    public Transform playerSpawnPoint;            // Spawn point for the player
    public GameObject playerInstance;            // Current player instance
    public float currentHealth;
    public int currentBulletDamage;
    public float speed;
    public float timeBetweenShots;

    public float maxHealth;
    public float remainingHealthPercentage;
    

    // Gameplay State
    [Header("Gameplay Settings")]
    public int level;                         // Current level
    public int score;                            // Current XP
    public int targetScore;               // XP needed to level up
    public CurrentLevelUI currentLevelUI;
    public ScoreUI currentScoreUI;

    // Enemy State
    [Header("Enemy Settings")]
    public GameObject enemySpawner;
    public float enemyMinSpawnTime;
    public float enemyMaxSpawnTime;


    // Camera
    [Header("Camera Settings")]
    public CinemachineVirtualCamera vCam;         // Virtual camera reference

    [Header("Audio Settings")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    private AudioSource audioSource;
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip shoot1Sound;
    [SerializeField] private AudioClip shoot2Sound;
    [SerializeField] private AudioClip shoot3Sound;
    [SerializeField] private AudioClip killSound;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip unlockWeaponSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;


    private void Awake()
    {
        // Ensure a single GameManager instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load events
        currentLevelUI = GetComponent<CurrentLevelUI>();
        currentScoreUI = GetComponent<ScoreUI>();

    }

    // Start is called before the first frame update
    private void Start()
    {
        InitializeGame();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid errors
    }

    private void InitializeGame()
    {
        // Destroy the player instance to ensure it doesn't persist into the main menu
        ResetGame();
        AssignCameraTarget();
    }

    private void SpawnPlayer()
    {
        Debug.Log("SpawnAndSetupPlayer() call");
        // Destroy existing player instance
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }

        // Instantiate and set up the player
        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }


    private void AssignCameraTarget() 
    {
        if (vCam != null && playerInstance != null)
        {
            // Set the camera to follow and look at the player
            vCam.Follow = playerInstance.transform;
            

            //// Ensure the camera confiner is updated
            //CinemachineConfiner confiner = vCam.GetComponent<CinemachineConfiner>();
            //if (confiner != null)
            //{
            //    // Find and assign the CameraBounds object
            //    PolygonCollider2D bounds = GameObject.Find("CameraBounds")?.GetComponent<PolygonCollider2D>();
            //    if (bounds != null)
            //    {
            //        confiner.m_BoundingShape2D = bounds;
            //        confiner.InvalidatePathCache(); // Refresh confiner for new bounds
            //    }
            //    else
            //    {
            //        Debug.LogWarning("CameraBounds object or PolygonCollider2D is missing!");
            //    }
            //}
        }
    }


    public void ChangeScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            
            SceneManager.LoadScene("MainMenu");
            ResetGame();
        }
        else
        {
            SceneManager.LoadScene(sceneName);
            //ResetGame();
        }
        
    }
   

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the name of the loaded scene and play the appropriate music
        if (scene.name == "MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (scene.name == "Level1")
            {
            PlayMusic(gameplayMusic);
            // After loading Level1, update the level and score UI
            UpdateLevelUI();
            UpdateScoreUI();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // Avoid restarting if the same music is already playing

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();

        }
    }

    // Specific sound triggers
    public void PlayShootSound()
    {
        if (level < 3)
        {
            PlaySound(shoot1Sound);
        } 
        else if (level >= 3 && level < 6)
        {
            PlaySound(shoot2Sound);
        } 
        else if (level >= 6 && level <= 10)
        {
            PlaySound(shoot3Sound);
        }
    }

    public void PlayKillSound() => PlaySound(killSound);
    public void PlayLevelUpSound() => PlaySound(levelUpSound);
    public void PlayUnlockWeaponSound() => PlaySound(unlockWeaponSound);
    public void PlayWinSound() => PlaySound(winSound);
    public void PlayLoseSound() => PlaySound(loseSound);

    

    public void ResetGame()
    {
        Debug.Log("Reset game call");
        currentHealth = 100f;
        maxHealth = 100f;
        currentBulletDamage = 10;
        speed = 4f;
        timeBetweenShots = 0.4f;

        level = 1;
        score = 0;
        targetScore = 100;

        enemyMinSpawnTime = 2f;
        enemyMaxSpawnTime = 6f;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            // Destroy the player instance to ensure it doesn't persist into the main menu
            if (playerInstance != null)
            {
                Destroy(playerInstance);
            }
        }
        else
        {
            SpawnPlayer();
        }
        AssignCameraTarget();
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.currentHealth = currentHealth;
        data.currentBulletDamage = currentBulletDamage;
        data.speed = speed;
        data.timeBetweenShots = timeBetweenShots;
        
        data.maxHealth = maxHealth;
        data.remainingHealthPercentage = remainingHealthPercentage;
        
        data.level = level;
        data.score = score;
        data.targetScore = targetScore;

        data.enemyMinSpawnTime = enemyMinSpawnTime;
        data.enemyMaxSpawnTime = enemyMaxSpawnTime;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Done Save Game");
    }
    public void LoadGame()
    {
        //Check If the save file is in the folder
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            // Move the information to Game Manager
            currentHealth = data.currentHealth;
            currentBulletDamage = data.currentBulletDamage;
            speed = data.speed;
            timeBetweenShots = data.timeBetweenShots;

            maxHealth = data.maxHealth;
            remainingHealthPercentage = data.remainingHealthPercentage;

            GameManager.Instance.level = data.level;
            GameManager.Instance.score = data.score;
            targetScore = data.targetScore;

            enemyMinSpawnTime = data.enemyMinSpawnTime;
            enemyMaxSpawnTime = data.enemyMaxSpawnTime;
            
            SceneManager.sceneLoaded += OnSceneLoaded;

            Debug.Log("Done Load Game");
            ChangeScene("Level1");
        }
    }
    private void UpdateLevelUI()
    {
        CurrentLevelUI levelUI = FindObjectOfType<CurrentLevelUI>();
        if (levelUI != null)
        {
            levelUI.UpdateLevel();
        }
    }

    private void UpdateScoreUI()
    {
        ScoreUI currentScoreUI = FindObjectOfType<ScoreUI>();
        if (currentScoreUI != null)
        {
            currentScoreUI.UpdateScore();
        }
    } 


}

[Serializable]
class PlayerData
{
        
    public float currentHealth;
    public int currentBulletDamage;
    public float speed;
    public float timeBetweenShots;

    public float maxHealth;
    public float remainingHealthPercentage;


    public int level;                         
    public int score;                            
    public int targetScore;              

    public float enemyMinSpawnTime;
    public float enemyMaxSpawnTime;
}

