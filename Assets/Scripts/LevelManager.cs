using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private Player player;
    private PlayerStateMachine playerStateMachine;

    public float waitToRespawn;

    public int gemsCollected;

    public string levelToLoad;

    public float timeInLevel;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = Player.Instance;
        playerStateMachine = new PlayerStateMachine();

        timeInLevel = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {

        Debug.Log("Entered respawn");
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack(); 

        player.gameObject.SetActive(false);
        //player.anim.SetBool("Dead", false);
        if (playerStateMachine != null )
        {
            Debug.Log("StateMachine");
            if (player.idleState != null )
            {
                Debug.Log("IdleState");
                playerStateMachine.ChangeState(player.idleState);
            }
        }
        player.gameObject.SetActive(true);
        player.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

        UIController.instance.UpdateHealthDisplay();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 0.2f);

        UIController.instance.FadeFromBlack();

        PlayerHealthController.instance.Ressurect();
        player.EnableUserInput();
    }

    public void EndLevel()
    {
        StartCoroutine (EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        AudioManager.instance.PlayLevelVictory();

        player.stopInput = true;

        CameraController.instance.stopFollow = true;

        UIController.instance.levelCompleteText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 2f);

        PlayerPrefs.SetInt (SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }
        }

        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene() + "_time"))
        {
            if(timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }
        
        

        SceneManager.LoadScene(levelToLoad);
    }
}
