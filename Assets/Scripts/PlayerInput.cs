using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class PlayerInput : MonoBehaviour
{
    private Camera _camera;
    private float xRotationMult = -5;
    private float yRotationMult = 1.5f;
    private float scaleFactor = 0.001f;
    float touchDist = 0;
    float lastDist = 0;
    float minScale = 0.03f;
    float maxScale = 0.1f;
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
                Transform castleParent = UIManager.instance.castleParent.transform;
                Transform castle = UIManager.instance.castle.transform;
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    castle.Rotate(0, touch.deltaPosition.x * Time.deltaTime*xRotationMult, 0, Space.Self);
                    castleParent.Rotate(0,0,touch.deltaPosition.y * Time.deltaTime*yRotationMult, Space.Self);
                }
                if (Input.touchCount == 2)
                {
                    Touch touch1 = Input.GetTouch(0);
                    Touch touch2 = Input.GetTouch(1);

                    if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
                    {
                        lastDist = Vector2.Distance(touch1.position, touch2.position);
                    }

                    if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                    {
                        float newDist = Vector2.Distance(touch1.position, touch2.position);
                        touchDist = lastDist - newDist;
                        lastDist = newDist;

                        // Your Code Here
                        if((touchDist > 0 && castle.localScale.x <= maxScale) || (touchDist < 0 && castle.localScale.x >= minScale))
                            castle.localScale += Vector3.one * touchDist * scaleFactor;
                        /*if (castle.localScale.x > maxScale)
                            castle.localScale = maxScale * Vector3.one;
                        if (castle.localScale.x < minScale)
                            castle.localScale = minScale * Vector3.one;*/

                    }
                }
                
                break;
        }
    }
}
