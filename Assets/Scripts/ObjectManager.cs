using ARLocation;
using ARLocation.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public double accuracyTreshold = 20;
    public List<PlaceAtLocation> locationBasedObjects;
    public GameObject poorConnectionIndicator;
    [HideInInspector] public int currentGhost = 0;
    public static ObjectManager instance;
    public LoadingBar distanceBar;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NextGhost()
    {
        currentGhost++;
        if(currentGhost >= locationBasedObjects.Count) 
            currentGhost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Coordinates location = new Coordinates(ARLocationProvider.Instance.CurrentLocation.latitude,
            ARLocationProvider.Instance.CurrentLocation.longitude);
        double currentAccuracy = ARLocationProvider.Instance.CurrentLocation.accuracy;
        poorConnectionIndicator.SetActive(currentAccuracy > accuracyTreshold);
        distanceBar.gameObject.SetActive(currentAccuracy <= accuracyTreshold);
        for(int i=0; i<locationBasedObjects.Count;i++) 
        {
            if(i != currentGhost)
                locationBasedObjects[i].gameObject.SetActive(false);
        }
        PlaceAtLocation obj = locationBasedObjects[currentGhost];
        if (currentAccuracy > accuracyTreshold)
        {
            obj.gameObject.SetActive(false);
            return;
        }
        if (obj.PlacementOptions.showDistance == 0)
            return;
        var distance =location.DistanceTo(new Coordinates(obj.LocationOptions.GetLocation().Latitude,
            obj.LocationOptions.GetLocation().Longitude), UnitOfLength.Kilometers )*1000f;
        distanceBar.FillPercentage = (float)distance / 100f;
        if (obj.PlacementOptions.showDistance > distance)
            obj.gameObject.SetActive(true);
        else if(obj.gameObject.activeSelf)
        {
            obj.GetComponent<PlacedObject>().Disappear();
        }
    }
}


public class Coordinates
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
public static class CoordinatesDistanceExtensions
{
    public static double DistanceTo(this Coordinates baseCoordinates, Coordinates targetCoordinates)
    {
        return DistanceTo(baseCoordinates, targetCoordinates, UnitOfLength.Kilometers);
    }

    public static double DistanceTo(this Coordinates baseCoordinates, Coordinates targetCoordinates, UnitOfLength unitOfLength)
    {
        var baseRad = Math.PI * baseCoordinates.Latitude / 180;
        var targetRad = Math.PI * targetCoordinates.Latitude / 180;
        var theta = baseCoordinates.Longitude - targetCoordinates.Longitude;
        var thetaRad = Math.PI * theta / 180;

        double dist =
            Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
            Math.Cos(targetRad) * Math.Cos(thetaRad);
        dist = Math.Acos(dist);

        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        return unitOfLength.ConvertFromMiles(dist);
    }
}

public class UnitOfLength
{
    public static UnitOfLength Kilometers = new UnitOfLength(1.609344);
    public static UnitOfLength NauticalMiles = new UnitOfLength(0.8684);
    public static UnitOfLength Miles = new UnitOfLength(1);

    private readonly double _fromMilesFactor;

    private UnitOfLength(double fromMilesFactor)
    {
        _fromMilesFactor = fromMilesFactor;
    }

    public double ConvertFromMiles(double input)
    {
        return input * _fromMilesFactor;
    }
}