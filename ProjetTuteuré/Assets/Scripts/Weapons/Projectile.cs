﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage;
    public float range;
    private Rigidbody2D rigidBody;
    float originCoordX, originCoordY;


    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        originCoordX = transform.position.x;
        originCoordY = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponentInParent<Enemy>().TakeDamage(damage, Vector3.zero,50, false);
            rigidBody.velocity = Vector2.zero;
        }
        Destroy(gameObject);
    }


    void Update () {
        //teste si le projectile a dépassé sa range, si oui il le détruit
        if ((float)System.Math.Sqrt(System.Math.Pow(originCoordX - transform.position.x, 2) + System.Math.Pow(originCoordY - transform.position.y, 2)) > range)
        {
            Destroy(gameObject);
        }
    }
}
