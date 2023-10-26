using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] soundEffects;

    public AudioSource bgm, levelEndMusic, bossMusic;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();

        soundEffects[soundToPlay].pitch = Random.Range(0.8f, 1f);

        soundEffects[soundToPlay].Play();
    }

    public void PlayLevelVictory ()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }

    public void PlayBossMusic()
    {
        bgm.Stop();
        bossMusic.Play();
    }

    public void StopBossMusic()
    {
        bossMusic.Stop();
        bgm.Play();
    }
}
