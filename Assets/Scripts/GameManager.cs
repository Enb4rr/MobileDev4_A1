using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Managers")]
    [SerializeField] private UIManager uiManager;

    [Header("Zones")]
    [SerializeField] private ExitZone[] exitZones;

    [Header("Scoring")]
    [SerializeField] private float baseScore = 300f;

    private int activeZoneIndex;
    private float elapsedTime;
    private bool isPlaying;

    // Read by UIManager
    public float ElapsedTime => elapsedTime;
    public int ActiveZoneIndex => activeZoneIndex;
    public bool IsPlaying => isPlaying;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Tell each zone what its index is
        for (int i = 0; i < exitZones.Length; i++)
            exitZones[i].ZoneIndex = i;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        activeZoneIndex = Random.Range(0, exitZones.Length);
        elapsedTime = 0f;
        isPlaying = true;

        // Tell all zones which one is active so they can visually highlight
        for (int i = 0; i < exitZones.Length; i++)
        {
            if (i == activeZoneIndex)
                exitZones[i].Activate();
            else
                exitZones[i].Deactivate();
        }
    }

    private void Update()
    {
        if (!isPlaying) return;
        elapsedTime += Time.deltaTime;
    }

    public void OnBallEnteredZone(int zoneIndex)
    {
        if (!isPlaying) return;
        if (zoneIndex != activeZoneIndex) return;

        isPlaying = false;
        int score = Mathf.Max(0, (int)(baseScore - elapsedTime));
        uiManager.ShowLevelComplete(elapsedTime, score);
    }

    public void RestartGame()
    {
        StartGame();
    }
}
