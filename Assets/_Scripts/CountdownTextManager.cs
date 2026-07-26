using EasyTextEffects;
using TMPro;
using UnityEngine;

public class CountdownTextManager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TextEffect_Base textEffect;
    private TextEffect _effect;

    void Awake()
    {
        _effect = text.GetComponent<TextEffect>();
    }

    void Start()
    {
        _effect.StartManualEffect(textEffect.effectTag);
        _effect.Refresh();
        _effect.Update(); //To fix a bug where a single-frame text is visible
    }

    void Update()
    {
        int currentTIme = (int)TimerManager.Instance.CurrentTime;
        if(currentTIme > 0)
        text.text = currentTIme.ToString();
        else
        text.text = $"You lose!\nExit by yourself there's no exit button";
    }
}
