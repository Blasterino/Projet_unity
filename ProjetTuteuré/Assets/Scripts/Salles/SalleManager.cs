﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SalleManager : MonoBehaviour {

    public bool roomEntre;
    public GameObject salleTiles, gameManager,room;
    public Porte porteOuest,porteNord,porteEst,porteSud;
    List<Porte> listePortes;
    public GameObject ennemiPrefab,ennemiPrefab2,boss;
    public Vector3 posCentre;
    public int nbEnnemisInSalle,ennemisDepart;
    public bool itemsDetruits,isRoomBoss;
    public List<GameObject> listeItem;
    public float timeAfterRoomEnd;//utilisé pour faire déspawn les items après un certain temps

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        listePortes = new List<Porte>();
        listePortes.Add(porteOuest);
        listePortes.Add(porteNord);
        listePortes.Add(porteEst);
        listePortes.Add(porteSud);

    }

    // Use this for initialization
    void Start () {
        roomEntre = false;
        listeItem = new List<GameObject>();
        itemsDetruits = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isRoomBoss)
        {
            if (!boss.GetComponent<Enemy>().isAlive)
            {
                gameManager.GetComponent<GameManager>().finirRoom(room);
                timeAfterRoomEnd = Time.time;
                roomEntre = false;
                foreach (Porte por in listePortes)
                {
                    if (por != null) { por.open(); }

                }
            }
        } else
        {
            if (checkEnnemis())
            {
                gameManager.GetComponent<GameManager>().finirRoom(room);
                timeAfterRoomEnd = Time.time;
                roomEntre = false;
                foreach (Porte por in listePortes)
                {
                    if (por != null) { por.open(); }

                }
            }
        }
	}

    public void debut(GameObject pl)
    {
        if (GetComponentInChildren<ExitLevel>()!= null)
        {
            GetComponentInChildren<ExitLevel>().setPlayer(pl);
        }
        
        if (!gameManager.GetComponent<GameManager>().isDebut(room) || isRoomBoss)
        {
            if (isRoomBoss)
            {
                roomEntre = true;
                foreach (Porte por in listePortes)
                {
                    if (por != null) { por.close(); }
                }
                return;
            }
            
            if (ennemisDepart == 0)
            {
                
                int nbEnnemis = Random.Range(0, 5);
                for (int k = 0; k < nbEnnemis; k++)
                {
                    int rand = (int)Random.Range(0f, 100f);
                    GameObject enemyInstancie;
                    if (rand > 20)
                    {
                        enemyInstancie = Instantiate(ennemiPrefab, posCentre, Quaternion.identity);
                    }
                    else
                    {
                        enemyInstancie = Instantiate(ennemiPrefab2, posCentre, Quaternion.identity);
                    }
                    enemyInstancie.GetComponent<Enemy>().manager = this;
                    AddEnemy();
                }
                roomEntre = true;
                foreach (Porte por in listePortes)
                {
                    if (por != null) { por.close(); }
                }
            } else
            {
                
                for (int k = 0; k < ennemisDepart; k++)
                {
                    int rand = (int)Random.Range(0f,100f);
                    GameObject enemyInstancie;
                    if (rand > 20)
                    {
                        enemyInstancie = Instantiate(ennemiPrefab, posCentre, Quaternion.identity);
                    } else
                    {
                        enemyInstancie = Instantiate(ennemiPrefab2, posCentre, Quaternion.identity);
                    }
                    
                    enemyInstancie.GetComponent<Enemy>().manager = this;
                    AddEnemy();
                }
                roomEntre = true;
                foreach (Porte por in listePortes)
                {
                    if (por != null) { por.close(); }
                }
            }
            
        }
        
    }

    public bool checkEnnemis()
    {
        if (!roomEntre)
        {
            return false;
        }

        return nbEnnemisInSalle == 0;
    }

    public void AddEnemy()
    {
        nbEnnemisInSalle += 1;
        
    }

    public void RemoveEnemy()
    {
        nbEnnemisInSalle--;
    }
}
