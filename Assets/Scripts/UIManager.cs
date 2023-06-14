using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("map menu")]
    TMP_Text mapHeading;
    [Header("ar menu")]
    public TMP_Text score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + GameManager.instance.score;
    }
}
