using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    PlayerFightingSystem fightingSystem;

    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * fightingSystem.force);
    }
}
