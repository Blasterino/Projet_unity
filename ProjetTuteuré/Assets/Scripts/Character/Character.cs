﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

    public float speed;

    protected Vector2 direction;

    public Animator animator;
    public Rigidbody2D rigidBody;
    public SpriteRenderer sprite;

    public GameObject hitValuePrefab;

    public bool isMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    public float maxHealth = 100f;
    public float currentHealth;

    protected bool isAlive;


    // Use this for initialization
    protected virtual void Start () {
        currentHealth = maxHealth;
        isAlive = true;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (!isAlive)
        {
            return;
        }
        HandleMovement();
	}

    public void HandleMovement()
    {
        if (!isAlive)
        {
            return;
        }
        if (isMoving)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", direction.y);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public virtual void Attack()
    {
        animator.SetTrigger("Attacking");
    }

    public virtual void DidHitObject(Collider collider, Vector3 hitVector)
    {
        Character character = collider.GetComponent<Character>();
        if(character != null && collider.tag != gameObject.tag)
        {
            if(collider.attachedRigidbody != null)
            {
                TakeDamage(10, hitVector);
            }
        }
    }

    protected virtual void Die()
    {
        isAlive = false;
        animator.SetBool("IsAlive", false);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Debug.Log("mort");
    }


    public virtual void TakeDamage(float damage, Vector3 hitVector)
    {
        rigidBody.AddForce(50 * hitVector);
        currentHealth -= damage;
        ShowHitEffects(damage, gameObject.transform.position);

        if (isAlive && currentHealth <= 0)
        {
            Die();
        }
    }

    public Vector2 getDirection()
    {
        return direction;
    }

    protected void ShowHitEffects(float value, Vector3 position)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("WorldCanvas");
        GameObject obj = Instantiate(hitValuePrefab);

        obj.transform.SetParent(canvas.transform, false);
        obj.transform.position = canvas.transform.position + position;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        obj.GetComponent<Text>().text = value.ToString();
        obj.GetComponent<DestroyTimer>().EnableTimer(1.0f);
    }

}
