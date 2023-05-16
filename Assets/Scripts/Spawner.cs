using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<GameObject> pedestrianPrefabs;
    public List<GameObject> vehiclePrefabs;
    public List<GameObject> airborneVehiclePrefabs;

    public Transform pedestrianSpawnPoint;
    public Transform vehicleSpawnPoint;
    public Transform airborneVehicleSpawnPoint;

    public float spawnRatePedestrian = 12f;
    public float spawnRateVehicle = 12f;
    public float spawnRateAirborneVehicle = 60f;

    public bool tutorial = false;
    
    void Start()
    {
        InvokeRepeating("SpawnPedestrian", 0f, spawnRatePedestrian);
        InvokeRepeating("SpawnVehicle", 0f, spawnRateVehicle);
        InvokeRepeating("SpawnAirborneVehicle", 0f, spawnRateAirborneVehicle);
    }

    void SpawnPedestrian()
    {
        // Instantiate a random pedestrian from the list at the pedestrian spawn point
        if (tutorial)   return;
        GameObject pedestrian = Instantiate(pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Count)], pedestrianSpawnPoint.position, pedestrianSpawnPoint.rotation);
    }

    void SpawnVehicle()
    {
        // Instantiate a random vehicle from the list at the vehicle spawn point
        GameObject vehicle = Instantiate(vehiclePrefabs[Random.Range(0, vehiclePrefabs.Count)], vehicleSpawnPoint.position, vehicleSpawnPoint.rotation);
    }

    void SpawnAirborneVehicle()
    {
        // Instantiate a random airborne vehicle from the list at the airborne vehicle spawn point
        GameObject airborneVehicle = Instantiate(airborneVehiclePrefabs[Random.Range(0, airborneVehiclePrefabs.Count)], airborneVehicleSpawnPoint.position, airborneVehicleSpawnPoint.rotation);
    }
}
