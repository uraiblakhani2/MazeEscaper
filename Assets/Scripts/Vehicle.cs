using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 30f;

    void Start()
    {
        // Destroy the car after a certain amount of time
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the car forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // If the NPC hits a barrier or the player, destroy it
        if (other.gameObject.CompareTag("Barrier") || other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}
