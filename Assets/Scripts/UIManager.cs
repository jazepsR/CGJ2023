using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;

public enum UIMode { map, AR}
public class UIManager : MonoBehaviour
{
    [Header("map menu")]
    public GameObject mapMenu;
    [SerializeField] private TMP_Text mapHeading;
    [SerializeField] private CastleController castleController;
    [SerializeField] private string[] castleFloorNames;

    [Header("ar menu")]
    public GameObject arMenu;
    public TMP_Text score;
    public TMP_Text time;

    [Header("Other")]
    public static UIManager instance;
    [HideInInspector] public UIMode viewerMode = UIMode.map;
    public GameObject ghostParent;
    public GameObject castleParent;
    public GameObject castle;
    public GameObject ghostMovedText;
    [HideInInspector] public float startTime = 0;
    
    private void Awake()
    {
        instance = this;
        viewerMode = UIMode.map;
        ToggleViewMode();
    }
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        viewerMode = UIMode.map;
        ToggleViewMode();
    }

    public void SetARMode()
    {
        viewerMode = UIMode.AR;
        ToggleViewMode();
    }

    public void SetMapMode()
    {
        viewerMode = UIMode.map;
        ToggleViewMode();
    }

    public void ToggleViewMode()
    {
        arMenu.SetActive(viewerMode == UIMode.AR);
        mapMenu.SetActive(viewerMode == UIMode.map);
        //arMenu.SetActive(viewerMode == UIMode.AR);
        castleParent.SetActive(viewerMode == UIMode.map);
        ghostMovedText.SetActive(false);
    }
    public string GetTimeString(float timeInSeconds)
    {
        return ((timeInSeconds) / 60).ToString("00") + ":" + ((timeInSeconds) % 60).ToString("00");
    }
    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + GameManager.instance.score;
        //time.text = "Time: "+ string.Format("{0:00}", (Time.time - startTime));
        time.text =  GetTimeString(Time.time - startTime);
        mapHeading.text = castleFloorNames[castleController.currentFloor];
    }
}
