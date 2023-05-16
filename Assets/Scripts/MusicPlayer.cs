using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; } // Singleton Instance

    void Awake() 
    {
        // If an Instance already exists and it's not this, destroy this
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject);
            return;
        } 

        // Set Instance to this
        Instance = this;

        // Make this object persist across scenes
        DontDestroyOnLoad(gameObject);
    }
}
