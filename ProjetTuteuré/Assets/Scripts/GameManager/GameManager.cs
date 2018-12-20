﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameObject[][] rooms;
    public MapPool poolMap;
    public ItemPool poolItem;
    bool[][] roomsFinies;
    public int[][] mapInit;
    List<int[]> listeFinale;
    public GameObject ennemiPrefab;
    public int maxEnnemis;

    // Use this for initialization
    void Start() {
        //Instanciations
        maxEnnemis = 3;
        mapInit = new int[5][];
        for (int i = 0; i < 5; i++)
        {
            mapInit[i] = new int[5];

        }
        listeFinale = new List<int[]>();
        rooms = new GameObject[5][];
        roomsFinies = new bool[5][];
        for (int i = 0; i < 5; i++)
        {
            rooms[i] = new GameObject[5];
            roomsFinies[i] = new bool[5];
        }
        genereMap();
        initieNiveau();

    }

    public void rechargerNiveau(int[][] map)
    {
        this.mapInit = map;
        initieNiveau();

    }


    public void initieNiveau()// Initie la map, génère les rooms
    {
        
        int[] req = new int[4];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                roomsFinies[i][j] = false;

                req = determineContraintes(i, j);//Détermination des conditions de génération req

                if (req != null)
                {
                    rooms[i][j] = poolMap.tire(req);//tire une room au hasard dans le pool, suivant les conditions req
                    creeRoom(i, j, rooms[i][j]);
                }
                
            }
        }
        creeRoom(5,4, poolMap.tire("0001"));

    }

    public void creeRoom(int i, int j, GameObject room)//instanciation d'une room aux coordonnées i,j
    {
        GameObject objinst;
        //génération de la salle
        Vector3 pos = new Vector3(i * 38, j * 26, 0);
        Vector3 posCentre = new Vector3(i * 38 +82, j * 26 -58, 0);
        objinst = Instantiate(room, pos, Quaternion.identity) as GameObject;
        objinst.transform.localScale = new Vector3(0.05f, 0.05f, 1);

        //Spawn des ennemis
        if(!(i == 0 && j == 0))
        {
            int nbEnnemis = UnityEngine.Random.Range(0, maxEnnemis);
            for (int k = 0; k < nbEnnemis; k++)
            {
                GameObject enemyInstancie = Instantiate(ennemiPrefab, posCentre, Quaternion.identity);
                enemyInstancie.GetComponent<Enemy>().manager = objinst.GetComponentInChildren<SalleManager>();
                objinst.GetComponentInChildren<SalleManager>().AddEnemy();
            }

            int chanceSpawnObjet = UnityEngine.Random.Range(0, 100);

            if(chanceSpawnObjet <= 15)
            {
                try
                {
                    Instantiate(poolItem.TireAndRemove(), posCentre, Quaternion.identity);
                }
                catch (ArgumentException e)
                {

                }
            }
        }
    }



    public int[] determineContraintes(int i, int j)
    {
        int[] req = new int[4];
        
        if(mapInit[i][j] == 0)
        {
            return null;
        }

        if (i == 0)
        {//si on est sur un bord
            req[3] = 0; }
        else
        { //cherche si la room située à l'ouest existe
            req[3] = mapInit[i-1][j]; }

        if (i == 4)//si on est sur un bord
        { req[2] = 0; }
        else
        {//cherche si la room située à l'est existe
            req[2] = mapInit[i + 1][j]; }

        if (j == 0)//si on est sur un bord
        { req[1] = 0; }
        else
        { //cherche si la room située au sud existe
            req[1] = mapInit[i][j-1]; }

        if (j == 4)//si on est sur un bord
        { req[0] = 0; }
        else
        {//cherche si la room située au nord existe
            req[0] = mapInit[i][j+1]; }

        if (i == 4 && j == 4)//si on est en haut à droite : une sortie est artificiellement demandée au cas ou la seule room possible soit 0000
        { req[2] = 1; }

        return req;
    }

    public void genereMap()
    {
        List<int[]> listMaze = new List<int[]>();
        int[] sortie = { 4, 4 };
        int[] tni = new int[2];

        while (!contient(listMaze,sortie))
        {
            listMaze = new List<int[]>();
            listeFinale = new List<int[]>();
            tni[0] = 0;
            tni[1] = 0;
            listMaze.Add(tni);
            regenereMapInit();
            listMaze = getMaze(0, 0);
        }
        purgeMapInit();
      
    }

    public List<int[]> getMaze(int i,int j)
    {
        List<int[]> liste = new List<int[]>();
        List<int[]> listeRetour;
        int[] tab = new int[2];
        tab[0] = i-1;
        tab[1] = j;
        
        if (i > 0 && mapInit[i - 1][j] == 1 && !contient(listeFinale,tab))
        {
            int[] tabAjout = { tab[0], tab[1] };
            liste.Add(tabAjout);
            this.listeFinale.Add(tabAjout);

            listeRetour = getMaze(i - 1, j);
            foreach (int[] tabb in listeRetour)
            {
               liste.Add(tabb);
            }
        }
        tab[0] = i + 1;
        if (i <4 && mapInit[i + 1][j] == 1 && !contient(listeFinale, tab))
        {
            int[] tabAjout = { tab[0], tab[1] };
            liste.Add(tabAjout);
            this.listeFinale.Add(tabAjout);

            listeRetour = getMaze(i + 1, j);
            foreach (int[] tabb in listeRetour)
            {
                liste.Add(tabb);

            }
        }
        tab[0] = i;

        tab[1] = j - 1;
        if (j > 0 && mapInit[i][j-1] == 1 && !contient(listeFinale, tab))
        {
            int[] tabAjout = { tab[0], tab[1] };
            liste.Add(tabAjout);
            this.listeFinale.Add(tabAjout);
            listeRetour = getMaze(i , j-1);
            foreach (int[] tabb in listeRetour)
            {
               liste.Add(tabb);

            }
        }

        tab[1] = j + 1;
        if (j < 4 && mapInit[i][j+1] == 1 && !contient(listeFinale, tab))
        {
            int[] tabAjout = { tab[0], tab[1] };
            liste.Add(tabAjout);
            this.listeFinale.Add(tabAjout);
            listeRetour = getMaze(i, j+1);
            foreach (int[] tabb in listeRetour)
            {
               liste.Add(tabb);

            }
        }
        return liste;
    }

    public bool contient(List<int[]> list, int[] tab)
    {
        foreach(int[] i in list)
        {
            if(i[0] == tab[0] && i[1] == tab[1])
            {
                return true;
            }
        }
        return false;
    }

    public int genereZeroOuUn()
    {
        float rand = UnityEngine.Random.Range(0,100);
        if(rand > 40)
        {
            return 0;
        } else
        {
            return 1;
        }
    }

    public void regenereMapInit()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                mapInit[i][j] = genereZeroOuUn();
                
            }
        }
        mapInit[0][0] = 1;
        mapInit[4][4] = 1;
    }

    public void purgeMapInit()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if(mapInit[i][j] == 1)
                {
                    int[] test = { i, j };
                    if (!contient(listeFinale, test))
                    {
                        mapInit[i][j] = 0;
                    }
                }
                
            }
        }
    }
}
