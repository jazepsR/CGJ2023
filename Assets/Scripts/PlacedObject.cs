using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    [HideInInspector] public bool disappearing = false;
    [HideInInspector] public bool collected = false;
    private Animator anim;
    [SerializeField] private float disableTime = 0.5f;
    [SerializeField] private float randomDisplacement = 7f;
    public Transform rootBone; 
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        disappearing = false;
    }

    private void Start()
    {
        anim.ResetTrigger("Disappear");
        Vector2 randomDir = Random.insideUnitCircle * randomDisplacement;
        rootBone.localPosition = new Vector3(randomDir.x, Random.Range(-2f,8f), randomDir.y);
        disappearing = false;
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
