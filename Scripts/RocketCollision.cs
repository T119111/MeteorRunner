using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    PlayerMovement playerMovement;

    public void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Player")
            playerMovement.Die();
    }
}
