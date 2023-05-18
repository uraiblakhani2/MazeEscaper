using UnityEngine;
using System.Collections;
using StarterAssets;

public enum PickupType
{
    Yellow,
    Blue,
    Red
}

// Pickup uses the same script for yellow, blue, and red interactable objects (including obstacles)
// with behavior OnTriggerEnter determined by the PickupType type. 

public class Pickup : MonoBehaviour
{
    public ParticleSystem particles;
    public PickupType type;
    public float rotationSpeed = 10f;

    // Slowly rotate on its y-axis
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particles.Play();

            switch(type)
            {
                case PickupType.Yellow:
                    Yellow();
                    break;
                case PickupType.Blue:
                    Blue();
                    break;
                case PickupType.Red:
                    Red();
                    break;
                default:
                    break;
            }
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(transform.parent.Find("Particle System").gameObject);
        }
    }

    private void Yellow()
    {
        GameManager.Instance.healthBoost();
        GameManager.Instance.AddYellowPickup();
        GameManager.Instance.AddScore(50);
    }

    private void Blue()
    {
        ThirdPersonController player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        player.bluePowerUp = true;
        player.hasDoubleJumped = false;
        GameManager.Instance.AddBluePickup();
        GameManager.Instance.AddScore(100);
        StartCoroutine(BlueRespawn());
    }

    private void Red()
    {
        GameManager.Instance.TakeDamage();
        Destroy(transform.parent.gameObject);
    }

    IEnumerator BlueRespawn()
    {
        yield return new WaitForSeconds(30f);
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
