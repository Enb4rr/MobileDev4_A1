using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController: MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));
    }
}