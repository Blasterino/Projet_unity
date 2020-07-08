﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class InventaireScript : MonoBehaviour {

    public static InventaireScript instance;

    public GameObject[] listeItems;
    public GameObject player;
    GameObject inventoryCanvas;

	// Use this for initialization
	void Start () {
        instance = this;

        listeItems = new GameObject[10];
        //GameObject i = (GameObject)Instantiate(Resources.Load("Jean"));
        setInventoryCanvas();
        updateMenuInventaire();
        //addItem(i);
        //updateMenuInventaire();

    }

    private void setInventoryCanvas()
    {
        inventoryCanvas = player.GetComponent<MenusJeu>().getInventoryCanvas();
    }

    public void save(String saveName)
    {
        Datas datas = (Datas)DataManager.Load(saveName);
        datas.nomsItems = new String[listeItems.Length];
        for (int i = 0; i < listeItems.Length; i++)
        {
            if ((listeItems[i]) != null)
            {
                datas.nomsItems[i] = listeItems[i].name;
                Debug.Log(datas.nomsItems[i]);
            }
        }
        DataManager.Save(datas,saveName);
    }

    public void load(String saveName)
    {
       
        Datas datas = (Datas)DataManager.Load(saveName);
        Debug.Log(datas);
        listeItems = new GameObject[10];
        for (int i = 0; i < datas.nomsItems.Length; i++)
        {
            //listeItems[i].name = datas.nomsItems[i];
            if ((datas.nomsItems[i]) != null)
            {
                Debug.Log(datas.nomsItems[i]);
                String[] ts = new String[2];
                ts = datas.nomsItems[i].Split('(');
                GameObject it = (GameObject)Instantiate(Resources.Load(ts[0]));
                addItem(it);
            }
        }
    }

    public bool isFull()
    {
        for (int i = 0; i < 10; i++)
        {
            if (listeItems[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void addItem(GameObject item)
    {
        for(int i = 0; i<10; i++)
        {
            if(listeItems[i] == null)
            {
                listeItems[i] = item;
                updateMenuInventaire();
                return;
            }
        }
        
    }

    //pour retirer un objet spécifique
    public void retirerItem(GameObject item)
    {
        for (int i = 0; i < 10; i++)
        {
            if (listeItems[i] == item)
            {
                if (player.GetComponent<Player>().armeCorpsACorpsEquipee != listeItems[i] && player.GetComponent<Player>().armeDistanceEquipee != listeItems[i])
                {
                    listeItems[i] = null;
                }

            }
        }
        updateMenuInventaire();
    }

    //pour retirer un objet à l'indice indice
    public void retirerItem(int indice)
    {
        if(player.GetComponent<Player>().armeCorpsACorpsEquipee != listeItems[indice] && player.GetComponent<Player>().armeDistanceEquipee != listeItems[indice])
        {
            listeItems[indice] = null;
        }
        updateMenuInventaire();
    }
	
    public void updateMenuInventaire()
    {
        //On update toutes les images des buttons de l'inventaire en trouvant le bouton dans les gameobjects puis en changeant leur sprite
        for (int i = 0; i < 10; i++)
        {
            GameObject buttonInv = inventoryCanvas.transform.GetChild(0).transform.GetChild(0).gameObject.transform.GetChild(i).gameObject;

            if (listeItems[i] != null)
            {
                buttonInv.GetComponent<Image>().sprite = listeItems[i].GetComponent<SpriteRenderer>().sprite;
            } else
            {
                buttonInv.GetComponent<Image>().sprite = null;
            }
        }



        //On cherche l'image de l'objet cac dans l'inventaire et on affiche le sprite correspondant
        GameObject imageCac = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(4).gameObject;
        imageCac.GetComponent<Image>().sprite = player.GetComponent<Player>().armeCorpsACorpsEquipee.GetComponent<SpriteRenderer>().sprite;

        //Pareil pour l'arme à distance
        GameObject imageDist = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(5).gameObject;
        imageDist.GetComponent<Image>().sprite = player.GetComponent<Player>().armeDistanceEquipee.GetComponent<SpriteRenderer>().sprite;

        //Pareil pour l'armure de tête
        GameObject imageHead = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(8).gameObject;
        imageHead.GetComponent<Image>().sprite = player.GetComponent<Player>().armureTeteEquipee.GetComponent<SpriteRenderer>().sprite;

        //Pareil pour l'armure de torse
        GameObject imageChest = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(9).gameObject;
        imageChest.GetComponent<Image>().sprite = player.GetComponent<Player>().armureTorseEquipee.GetComponent<SpriteRenderer>().sprite;

        //Pareil pour l'armure de jambes
        GameObject imageLegs = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(10).gameObject;
        imageLegs.GetComponent<Image>().sprite = player.GetComponent<Player>().armureJambesEquipee.GetComponent<SpriteRenderer>().sprite;


        //On update les stats dus au changement d'armure
        updateStats();

        //On change la force
        GameObject texteForce = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(1).gameObject;
        texteForce.GetComponent<Text>().text = "Strength : "+Convert.ToInt32(player.GetComponent<Player>().strength);

        //L'endurance
        GameObject texteEndu = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(2).gameObject;
        texteEndu.GetComponent<Text>().text = "Endurance : " + Convert.ToInt32(player.GetComponent<Player>().endurance);

        //Et l'agilité
        GameObject texteAgi = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(3).gameObject;
        texteAgi.GetComponent<Text>().text = "Agility : " + Convert.ToInt32(player.GetComponent<Player>().agility);

        GameObject textGold = inventoryCanvas.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(11).gameObject;
        textGold.GetComponent<Text>().text = "Gold: " + player.GetComponent<Player>().gold + " g";

    }

    public void updateStats()
    {
        //On update la force du joueur en fonction de ses armures equipees
        GetComponent<Player>().strength = GetComponent<Player>().basestrength + 
        GetComponent<Player>().armureTeteEquipee.GetComponent<HeadArmor>().strength +
        GetComponent<Player>().armureTorseEquipee.GetComponent<BodyArmor>().strength +
        GetComponent<Player>().armureJambesEquipee.GetComponent<LegArmor>().strength;
        //Debug.Log(GetComponent<Player>().armureTeteEquipee.GetComponent<HeadArmor>().strength);
        //Debug.Log(GetComponent<Player>().armureTeteEquipee.GetComponent<BodyArmor>().strength);
        //Debug.Log(GetComponent<Player>().armureTeteEquipee.GetComponent<LegArmor>().strength);

        //idem pour l'agilite
        GetComponent<Player>().agility = GetComponent<Player>().baseagility +
        GetComponent<Player>().armureTeteEquipee.GetComponent<HeadArmor>().agility +
        GetComponent<Player>().armureTorseEquipee.GetComponent<BodyArmor>().agility +
        GetComponent<Player>().armureJambesEquipee.GetComponent<LegArmor>().agility;

        //idem pour l'endurance
        GetComponent<Player>().endurance = GetComponent<Player>().baseendurance +
        GetComponent<Player>().armureTeteEquipee.GetComponent<HeadArmor>().endurance +
        GetComponent<Player>().armureTorseEquipee.GetComponent<BodyArmor>().endurance +
        GetComponent<Player>().armureJambesEquipee.GetComponent<LegArmor>().endurance;

        GetComponent<Player>().maxHealth = GetComponent<Player>().baseHealth + GetComponent<Player>().endurance;
        GetComponent<Player>().activeSpeed = GetComponent<Player>().speed + GetComponent<Player>().agility * 0.1f;
    }

    public bool isInInventaire(GameObject obj)
    {
        foreach(GameObject objTest in listeItems)
        {
            if (objTest!=null && objTest.Equals(obj))
            {
                return true;
            }
        }
        return false;
    }
}
