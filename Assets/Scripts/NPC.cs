using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public float speed = 3f;
    public float followTime = 10f;
    public float detectionRadius = 5f;
    private bool isFollowingPlayer = false;
    private GameObject player;
    private Rigidbody rb;

    public bool tutorial = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            // Follow the player
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction); // Rotate towards player
            transform.position += direction * speed * Time.deltaTime;
            // rb.velocity = direction * speed;
        }
        else
        {
            // Move in a straight line
            if (!tutorial)
                transform.position += transform.forward * speed * Time.deltaTime;
            // rb.velocity = transform.forward * speed;
        }

        // Check if the player is close
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
        {
            isFollowingPlayer = true;
            StartCoroutine(StopFollowingPlayer());
        }
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

    IEnumerator StopFollowingPlayer()
    {
        yield return new WaitForSeconds(followTime);
        isFollowingPlayer = false;
        Destroy(gameObject);
    }
}
