using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject levelCompletePanel;

    [Header("HUD")]
    [SerializeField] private TMP_Text timerLabel;
    [SerializeField] private TMP_Text activeZoneLabel;

    [Header("Level Complete")]
    [SerializeField] private TMP_Text finalTimeLabel;
    [SerializeField] private TMP_Text finalScoreLabel;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(OnRestartPressed);
        ShowHUD();
    }

    private void Update()
    {
        // Continuously update timer while playing
        if (hudPanel.activeSelf && GameManager.Instance.IsPlaying)
            timerLabel.text = $"Time: {GameManager.Instance.ElapsedTime:F1}s";
    }

    public void ShowHUD()
    {
        hudPanel.SetActive(true);
        levelCompletePanel.SetActive(false);

        int zoneIndex = GameManager.Instance.ActiveZoneIndex;
        activeZoneLabel.text = $"Find Exit {zoneIndex + 1}";
    }

    public void ShowLevelComplete(float time, int score)
    {
        hudPanel.SetActive(false);
        levelCompletePanel.SetActive(true);

        finalTimeLabel.text = $"Time: {time:F1} s";
        finalScoreLabel.text = $"Score: {score}";
    }

    private void OnRestartPressed()
    {
        GameManager.Instance.RestartGame();
        ShowHUD();
    }
}
