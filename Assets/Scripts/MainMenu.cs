using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip introNarration;
    public GameObject textMenu;
    public CanvasGroup textMenuCanvas;
    public AnimationCurve fadeCurve;
    // Start is called before the first frame update
    void Start()
    {
        textMenu.SetActive(false); textMenuCanvas.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToText()
    {
        MusicController.instance.PlaySound(introNarration);
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
