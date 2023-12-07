using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Player player;

    public static CheckpointController instance;

    public Checkpoint[] checkpoints;

    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
        checkpoints = Object.FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
        spawnPoint = player.transform.position;
    }

    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
