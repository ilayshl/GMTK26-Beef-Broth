using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public float Score => _score;
    private float _score = 0f;

    public void Reset()
    {
        _score = 0f;
    }

    public void UpdateScore(int addition)
    {
        _score += addition;
    }
}
