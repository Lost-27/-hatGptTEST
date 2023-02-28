using UnityEngine;

public class EnemyPush : MonoBehaviour
{
    public float pushForce = 10.0f;
    public float pushTime = 1.0f;
    private CharacterController characterController;
    private float pushTimer = 0.0f;
    private Vector3 pushDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            characterController = other.gameObject.GetComponent<CharacterController>();

            pushDirection = other.gameObject.transform.position - transform.position;
            pushDirection = pushDirection.normalized;
        }
    }

    private void Update()
    {
        if (pushTimer > 0)
        {
            pushTimer -= Time.deltaTime;
            characterController.Move(pushDirection * pushForce * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pushTimer = pushTime;
        }
    }
}
