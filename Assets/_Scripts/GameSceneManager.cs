using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    void Start()
    {
        CharacterSpawner.Instance.SpawnPlayer();
        Invoke(nameof(Invoked), 2f);
    }

    private void Invoked()
    {
        CharacterSpawner.Instance.SpawnEnemy();
    }
}
