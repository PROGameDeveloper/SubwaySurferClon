using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] PlayerCollision playerCollision;
    void Awake()
    {
        playerCollision = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollision>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        playerCollision.OnCharacterCollision(collision.collider);
    }
}
