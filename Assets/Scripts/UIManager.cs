using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum UIMode { map, AR}
public class UIManager : MonoBehaviour
{
    [Header("map menu")]
    public GameObject mapMenu;
    TMP_Text mapHeading;

    [Header("ar menu")]
    public GameObject arMenu;
    public TMP_Text score;

    [Header("Other")]
    public static UIManager instance;
    [HideInInspector] public UIMode viewerMode = UIMode.map;
    public GameObject ghostParent;
    public GameObject castleParent;
    public GameObject castle;
    private void Awake()
    {
        instance = this;
        viewerMode = UIMode.map;
        ToggleViewMode();
    }
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + GameManager.instance.score;
    }
}
