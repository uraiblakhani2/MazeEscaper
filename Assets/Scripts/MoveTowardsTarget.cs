using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
            // Destroy the object when it reaches its target
            if(Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
