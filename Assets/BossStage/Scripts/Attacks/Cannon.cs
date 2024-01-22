using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // VARIABLES:
    public GameObject CannonBullet;
    private GameObject empty;

    public float Speed;

    private void Awake()
    {
        // preserve the bullets absolute proportions
        empty = GameObject.FindGameObjectWithTag("Empty");
        if (empty == null)
            Debug.LogError("CANNON: Empty not found");
    }

    public void Shoot(Vector2 shootingVector)
    {
        GameObject go = Instantiate(CannonBullet,
                                    this.transform.position,
                                    empty.transform.rotation,
                                    empty.transform) as GameObject;
        go.transform.parent = empty.transform;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        //rb.MoveRotation(Mathf.Atan2(shootingVector.y, shootingVector.x) * Mathf.Rad2Deg);
        rb.velocity = shootingVector * Speed;
    }
}
