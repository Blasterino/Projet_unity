﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {

    public float speed;

    protected Vector2 direction;

    public Animator animator;
    public Rigidbody2D rigidBody;
    public SpriteRenderer sprite;

    public GameObject hitValuePrefab;

    private GameObject canvas;

    public bool isMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    public float baseHealth;
    public float maxHealth;
    public float currentHealth;

    public bool isAlive;

    public LifeBar lifeBar;

    protected virtual void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("WorldCanvas");
    }

    // Use this for initialization
    protected virtual void Start () {
        currentHealth = maxHealth;
        isAlive = true;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
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

    public virtual void Attack(float range)
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
                TakeDamage(10, hitVector, 10, false);
            }
        }
    }

    public virtual void Die()
    {
        isAlive = false;
        animator.SetBool("IsAlive", false);
        //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        /*
        if(gameObject.GetComponent<BoxCollider2D>() != null)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        } else
        {
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
        }
        */
    }


    public virtual void TakeDamage(float damage, Vector3 hitVector, float force, bool crit)
    {
        //force /= 100;
        rigidBody.AddForce(force * hitVector, ForceMode2D.Force);
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        ShowHitEffects(damage, gameObject.transform.position, crit);

        if (isAlive && currentHealth <= 0)
        {
            Die();
        }

        lifeBar.SetProgress(currentHealth / maxHealth);
        Color color = sprite.color;
        if (currentHealth < 0)
        {
            color.a = 0.75f;
        }
    }

    public Vector2 getDirection()
    {
        return direction;
    }

    protected void ShowHitEffects(float value, Vector3 position, bool crit)
    {
        Vector3 rndPos = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);

        GameObject obj = Instantiate(hitValuePrefab);
        
        obj.GetComponent<Text>().text = value.ToString();
        if (crit)
        {
            obj.GetComponent<Text>().color = Color.red;
        }
        obj.GetComponent<DestroyTimer>().EnableTimer(1.0f);
        obj.GetComponent<MoveUpper>().moveDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 1, 0);

        obj.transform.SetParent(canvas.transform, false);
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
        obj.transform.position = position+rndPos;

    }

    public GameObject getCanvas()
    {
        return this.canvas;
    }

}
