﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBossIA : MonoBehaviour
{
    public GameObject bebons;//les bebons qu'il balance
    public GameObject flammes;//les flammes qu'il balance
    public GameObject orbe;//orbe qu'il balance
    public int state,attackType;
    public float attackTime,jumpTime,startPos,flammeTime,attackCooldown;
    bool attackTir1, attackTir2, attackTir3;//bools indiquant si les deux tirs de l'attaque 0 ont été faits

    // Start is called before the first frame update
    void Start()
    {
        state = -1;
        startPos = gameObject.transform.position.y;
        GetComponent<Enemy>().updateEnemyHP(GameObject.Find("GameManager").GetComponent<GameManager>().numeroNiveau / 10 * 100 + 400);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameObject.GetComponent<Enemy>().isAlive)
        {
            gameObject.GetComponent<Animator>().SetBool("IsAlive", false);
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            return;
        }
        if(state == 0)
        {
            if (Time.time - jumpTime > 1)
            {
                state = 1;
                GetComponent<Animator>().SetBool("hasFinishedJump", true);
                attackTime = Time.time;
                attackTir1 = attackTir2 = attackTir3 = false;
                flammeTime = 0;
                attackType = (int)Random.Range(0, 3);
                return;
            } else
            {
                Vector3 pos = new Vector3(gameObject.transform.position.x, startPos + 3-(Mathf.Pow(1 - (Time.time-jumpTime)*2, 2)*3),0);
                gameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
            }
        }
        if(state == 1)
        {
            if (Time.time - attackCooldown > 3)
            {
                if (Time.time - attackTime > 5f)
                {
                    attackTime = Time.time;
                    attackTir1 = attackTir2 = attackTir3 = false;
                    flammeTime = 0;
                    attackType = (int)Random.Range(0, 3);
                }
                if (attackType == 0)
                {// attaque de bombes
                    if (!attackTir1 && Time.time - attackTime > 1f)
                    {
                        attackTir1 = true;
                        GameObject bebon1 = Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.8f, 0), Quaternion.identity);
                        bebon1.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, -10);
                        bebon1.GetComponent<Bombe>().isCross = true;
                        GameObject bebon2 = Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.8f, 0), Quaternion.identity);
                        bebon2.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                        bebon2.GetComponent<Bombe>().isCross = true;
                        GameObject bebon3 = Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.8f, 0), Quaternion.identity);
                        bebon3.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 10);
                        bebon3.GetComponent<Bombe>().isCross = true;
                    }
                    else if (!attackTir2 && Time.time - attackTime > 2.5f)
                    {
                        attackTir2 = true;
                        Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-10, -5);
                        Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-10, -15);
                        Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 5);
                        Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 15);
                    }
                    else if (!attackTir3 && Time.time - attackTime > 4)
                    {
                        attackTir3 = true;
                        GameObject bebon1 = Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.8f, 0), Quaternion.identity);
                        bebon1.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, -10);
                        bebon1.GetComponent<Bombe>().isCross = true;
                        GameObject bebon2 = Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.8f, 0), Quaternion.identity);
                        bebon2.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                        bebon2.GetComponent<Bombe>().isCross = true;
                        GameObject bebon3 = Instantiate(bebons, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.8f, 0), Quaternion.identity);
                        bebon3.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 10);
                        bebon3.GetComponent<Bombe>().isCross = true;
                        attackCooldown = Time.time;
                    }
                }
                else if (attackType == 1)//attaque lance-flammes
                {
                    if (Time.time - attackTime > flammeTime)
                    {
                        Vector3 playerPos = GameObject.Find("Player").transform.position;

                        flammeTime += 0.5f;

                        Instantiate(flammes, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-10, playerPos.y - gameObject.transform.position.y);
                        Instantiate(flammes, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 10);
                        Instantiate(flammes, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-10, -10);


                    }
                }
                else
                {
                    if (!attackTir1)
                    {
                        attackTir1 = true;
                        attackTime = Time.time;
                        Instantiate(orbe, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
                        if (Time.time - attackTime > 4.9)
                        {
                            attackCooldown = Time.time;
                        }
                    }
                }
            } 
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(10,Vector3.zero,10,false);
        }
    }
}
