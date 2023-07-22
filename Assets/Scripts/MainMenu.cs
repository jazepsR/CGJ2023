using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject textMenu;
    public CanvasGroup textMenuCanvas;
    public AnimationCurve fadeCurve;


    void Start()
    {
        textMenu.SetActive(false); textMenuCanvas.alpha = 0;
    }

    async void Awake()
    {
        await UnityServices.InitializeAsync();

        await SignInAnonymously();
    }

    async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            // Take some action here...
            Debug.Log(s);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    public void GoToText()
    {
        textMenu.SetActive(true);
        StartCoroutine(FadeInTextMenu());
    }

    private IEnumerator FadeInTextMenu()
    {
        float t = 0;
        while(t<1)
        {
            textMenuCanvas.alpha = fadeCurve.Evaluate(t);
            t += Time.deltaTime;
            yield return null;
        }
        textMenuCanvas.alpha = 1;
    }
    public void StartGame()
    {
        MusicController.instance.soundFX.Stop();
        SceneManager.LoadScene(1);
    }
}
