using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game Scene";
    [SerializeField] private Button playBtn, settingsBtn, creditsBtn;

    void Awake()
    {
        playBtn.onClick.AddListener(OnPlayPressed);
        settingsBtn.onClick.AddListener(OnSettingsPressed);
        creditsBtn.onClick.AddListener(OnCreditsPressed);
    }

    void OnDestroy()
    {
        playBtn.onClick.RemoveListener(OnPlayPressed);
        settingsBtn.onClick.RemoveListener(OnSettingsPressed);
        creditsBtn.onClick.RemoveListener(OnCreditsPressed);
    }

    private void OnPlayPressed()
    {
        SceneManager.LoadScene("Game Scene");
    }

    private void OnSettingsPressed()
    {
        throw new NotImplementedException();
    }

    private void OnCreditsPressed()
    {
        throw new NotImplementedException();
    }
}
