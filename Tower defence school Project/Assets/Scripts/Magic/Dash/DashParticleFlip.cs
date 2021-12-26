using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticleFlip : MonoBehaviour
{
    [SerializeField] private Transform player;
    public bool isFlipped = false;

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= 1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

    }
}
