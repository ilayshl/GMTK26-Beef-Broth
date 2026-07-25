using UnityEngine;

public class FloorAlternator : MonoBehaviour
{
    [SerializeField] private bool crossfadeSprites = true;
    [SerializeField] private SpriteRenderer spriteA;
    [SerializeField] private SpriteRenderer spriteB;
    [SerializeField] private float crossDuration = 1.5f;
    [SerializeField] private float staggerTime;
    [SerializeField] private bool isRandomStagger;

    private void Awake()
    {
        SetAlpha(spriteA, 1f);
        SetAlpha(spriteB, 0f);
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time / crossDuration, 1f);

        SetAlpha(spriteA, 1f - t);
        SetAlpha(spriteB, t);
    }

    private void SetAlpha(SpriteRenderer renderer, float alpha)
    {
        Color color = renderer.color;
        color.a = alpha;
        renderer.color = color;
    }
}
