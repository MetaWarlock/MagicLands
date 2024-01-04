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
        if (Input.GetKeyDown(KeyCode.P)) // ������ ������� ������ ������� ��� �����
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
        Time.timeScale = 0f; // ���������������� ����� (����)
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // ������������ ����� (����)
        isPaused = false;
    }
}