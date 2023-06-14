using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class PlayerInput : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Ray ray;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                ray = Camera.main.ScreenPointToRay(touch.position);
            }
            else
            {//keyboard
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "ghost")
                {
                    //TODO: mark ghost as completed
                    PlacedObject obj = hit.transform.GetComponent<PlacedObject>();
                    if (obj)
                    {
                        if (obj.disappearing!)
                        {
                            obj.Disappear();
                            GameManager.instance.IncreaseScore();
                            ObjectManager.instance.NextGhost();
                        }
                        obj.disappearing = true;
                    }
                }
            }
        }
    }
}
