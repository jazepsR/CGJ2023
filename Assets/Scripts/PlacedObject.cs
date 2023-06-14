using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    [HideInInspector] public bool disappearing = false;
    private Animator anim;
    [SerializeField] private float disableTime = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.LogError("starting");
        anim.ResetTrigger("Disappear");
    }

    // Update is called once per frame


    public void Disappear()
    {
        if(!disappearing)
        {
            disappearing = true;
            anim.SetTrigger("Disappear");
            Invoke("DisableObj", disableTime);
        }
    }

    private void DisableObj()
    {
        disappearing = false;
        gameObject.SetActive(false);
    }
}
