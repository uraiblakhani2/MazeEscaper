using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.score >= 500 && GameManager.Instance.HasKey)
            {
                Debug.Log("Finish Stage");
                GameManager.Instance.FinishStage();
            }
        }
    }
}
