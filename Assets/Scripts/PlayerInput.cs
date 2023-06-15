using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class PlayerInput : MonoBehaviour
{
    private Camera _camera;
    private float xRotationMult = 5;
    private float yRotationMult = 1;
    private float scaleFactor = 0.05f;
    float touchDist = 0;
    float lastDist = 0;
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        switch(UIManager.instance.viewerMode)
        {
            case UIMode.AR:
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
                                if (obj.collected == false)
                                {
                                    obj.Disappear();
                                    GameManager.instance.IncreaseScore();
                                    ObjectManager.instance.NextGhost();
                                    obj.collected = true;
                                }
                            }
                        }
                    }
                }
                break;
            case UIMode.map:
                Transform castle = UIManager.instance.castleParent.transform;
                if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
                {
                    if (Input.touchCount == 1)
                    {
                        Touch touch = Input.GetTouch(0);
                        castle.Rotate(0, touch.deltaPosition.x * Time.deltaTime*xRotationMult, 0, Space.Self);
                        castle.Rotate(touch.deltaPosition.y * Time.deltaTime*yRotationMult,0 , 0, Space.World);
                    }
                    if (Input.touchCount == 2)
                    {
                        Touch touch1 = Input.GetTouch(0);
                        Touch touch2 = Input.GetTouch(1);

                        if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
                        {
                            lastDist = Vector2.Distance(touch1.position, touch2.position);
                        }

                        if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                        {
                            float newDist = Vector2.Distance(touch1.position, touch2.position);
                            touchDist = lastDist - newDist;
                            lastDist = newDist;

                            // Your Code Here
                            castle.localScale += Vector3.one* touchDist * scaleFactor;
                        }
                    }
                }
                break;
        }
    }
}
