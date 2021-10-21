using UnityEngine;

public class HordManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnAreas;
    [SerializeField] private GameObject spawnObj;
    [SerializeField] private int spawnCount, waveCount;
    [SerializeField] private float spawnDelay;
    private int _currentWave;
    private float _timer;
    private ZombieAIManager _zombieAIManager;

    private void Start()
    {
        spawnAreas = GetComponentsInChildren<Transform>();
        _zombieAIManager = GetComponent<ZombieAIManager>();
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver) return;

        _timer -= Time.deltaTime;
        if (_timer < 0 && _currentWave < waveCount)
        {
            var randArea = Random.Range(0, spawnAreas.Length);

            for (int i = 0; i < spawnCount; i++)
            {
                var randX = Random.Range(1, 15);
                var randZ = Random.Range(1, 15);

                var currentZombie = GameManager.Instance.poolManager.CheckPool(spawnObj);
                currentZombie.transform.position =
                    new Vector3(spawnAreas[randArea].position.x + randX, spawnAreas[randArea].position.y,
                        spawnAreas[randArea].position.z + randZ);
                currentZombie.transform.rotation = spawnAreas[randArea].rotation;
                currentZombie.SetActive(true);
                _zombieAIManager.zombieAIs.Add(currentZombie.GetComponent<ZombieAI>());
                _zombieAIManager.zombies.Add(currentZombie.GetComponent<Zombie>());
            }

            _timer = spawnDelay;
            _currentWave++;
        }
        else
        {
            foreach (var zombie in _zombieAIManager.zombies)
            {
                if (zombie.isActiveAndEnabled) return;
            }

            GameManager.GameOver?.Invoke(true);
            // Debug.Log("win");
        }
    }
}