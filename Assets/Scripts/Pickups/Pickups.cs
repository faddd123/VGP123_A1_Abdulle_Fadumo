using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum CollectibleType
    {
        POWERUP,
        COLLECTIBLE,
        LIVES,
        KEY
    }
    public CollectibleType currentCollectible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            switch (currentCollectible)
            {
                case CollectibleType.POWERUP:
                    Debug.Log("Powerup");
                   // collision.GetComponent<PlayerMovement>().StartJumpForceChange();
                    Destroy(gameObject);
                    break;
                case CollectibleType.LIVES:
                    Debug.Log("Lives");
                    collision.GetComponent<PlayerMovement>().lives++;
                    Destroy(gameObject);
                    break;
                case CollectibleType.COLLECTIBLE:
                    Debug.Log("Collectible");
                    collision.GetComponent<PlayerMovement>().score++;
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
