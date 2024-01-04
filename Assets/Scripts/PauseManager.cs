using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static PauseManager instance;

    private bool isPaused = false;

    public static PauseManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Можете выбрать другую клавишу для паузы
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }*/

    public void PauseGame()
    {
        Time.timeScale = 0f; // Приостанавливаем время (игру)
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Возобновляем время (игру)
        isPaused = false;
    }
}