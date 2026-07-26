using CheesePull;
using UnityEngine;

public class CharacterSpawner : Singleton<CharacterSpawner>
{
    public Rigidbody SpawnedPlayer {get; private set;}
    [SerializeField] private CarBrain playerPrefab;
    [SerializeField] private CarBrain enemyPrefab;

    public void SpawnPlayer()
    {
        var spawned = ObjectPoolManager.SpawnObject(playerPrefab, Vector3.zero, Quaternion.identity, PoolType.GameObjects);
        spawned.IsPlayer = true;
        SpawnedPlayer = spawned.GetComponent<Rigidbody>();
    }

    public void SpawnEnemy()
    {
        var spawned = ObjectPoolManager.SpawnObject(enemyPrefab, Vector3.zero, Quaternion.identity, PoolType.GameObjects);
        spawned.IsPlayer = false;
    }
}
