using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    public LSPlayer thePlayer;

    private MapPoint[] allPoints;

    // Start is called before the first frame update
    void Start()
    {
        allPoints = Object.FindObjectsByType<MapPoint>(FindObjectsSortMode.InstanceID);

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach (MapPoint point in allPoints)
            {
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.currentPoint = point;
                    thePlayer.transform.position = point.transform.position;
                    
                }
            }
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        AudioManager.instance.PlaySFX(4);

        LSUIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / LSUIController.instance.fadeSpeed) + 0.25f);

        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
