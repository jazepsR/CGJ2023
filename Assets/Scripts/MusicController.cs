using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource mapAmbience;
    public AudioSource arAmbience;
    public AudioSource soundFX;
    private float lerpTime = 1f;
    [HideInInspector] public bool isMap = true;
    public static MusicController instance;
    void Awake()
    {        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleAmbience(bool isMap)
    {
        this.isMap = isMap;
    }

    public void PlaySound(AudioClip clip)
    {
        soundFX.PlayOneShot(clip);
    }

    void Update()
    {
        mapAmbience.volume = Mathf.Lerp(mapAmbience.volume, isMap ? 0.5f : 0, Time.deltaTime * lerpTime);
        arAmbience.volume = Mathf.Lerp(mapAmbience.volume, isMap ? 0 : 0.5f, Time.deltaTime * lerpTime);
    }
}
