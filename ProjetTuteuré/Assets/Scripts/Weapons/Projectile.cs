﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage;
    public float range;
    private Rigidbody2D rigidBody;
    float originCoordX, originCoordY, time;
    public bool isCrit,isFriendly,isSlash;


    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        originCoordX = transform.position.x;
        originCoordY = transform.position.y;
        time = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag != "Projectile")
        {
            if (collision.tag == "Enemy" && isFriendly)
            {
                if (collision.GetComponent<Enemy>().isAlive)
                {
                    //TODO : knockback
                    collision.GetComponentInParent<Enemy>().TakeDamage(damage, Vector3.zero, 50, isCrit);

                    if (!isSlash)
                    {
                        Destroy(gameObject);
                    }
                }
                
                
            } else if (collision.tag == "Player" && !isFriendly)
            {
                //TODO : knockback
                collision.GetComponentInParent<Player>().TakeDamage(damage, Vector3.zero, 50, isCrit);
                rigidBody.velocity = Vector2.zero;
                if (!isSlash)
                {
                Destroy(gameObject);
                }
                
            }

            if(collision.tag == "Collision")
            {
                Destroy(gameObject);
            }
            
        }
        
    }


    void FixedUpdate () {
        //teste si le projectile a dépassé sa range, si oui il le détruit
        if ((float)System.Math.Sqrt(System.Math.Pow(originCoordX - transform.position.x, 2) + System.Math.Pow(originCoordY - transform.position.y, 2)) > range)
        {
            Destroy(gameObject);
        }
        if (isSlash)
        {
            if (Time.time - time > 0.1)
            {
                Destroy(gameObject);
            }
        }
    }
}
