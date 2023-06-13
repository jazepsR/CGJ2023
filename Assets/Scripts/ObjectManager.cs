using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<PlaceAtLocation> locationBasedObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2d location = new Vector2d(ARLocationProvider.Instance.CurrentLocation.latitude,
            ARLocationProvider.Instance.CurrentLocation.longitude);
        foreach (PlaceAtLocation obj in locationBasedObjects)
        {
            if(obj.PlacementOptions.showDistance ==0)
                continue;
            obj.gameObject.SetActive(obj.PlacementOptions.showDistance < Vector2d.Distance(location, new Vector2d(obj.LocationOptions.GetLocation().Latitude,
                obj.LocationOptions.GetLocation().Longitude)));        
                
        }
    }
}
