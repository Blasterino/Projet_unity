using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public static Shop instance;

    public GameObject shopMenu, buyMenu, sellMenu;

    public Text goldText;

    public List<object[]> itemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public GameObject selectedItem;
    public Text buyItemName, buyItemDescription, buyItemPrice;
    public Text sellItemName, sellItemDescription, sellItemPrice;

    public InventaireScript inventaire;

    // Start is called before the first frame update
    void Start()
    {
        //je comprend pas l'instance
        instance = this;
        //on commence tout désactivé
        shopMenu.SetActive(false);
        buyMenu.SetActive(false);
        sellMenu.SetActive(false);
    }

    public void OpenShop()
    {
        //on active l'UI et on active le menu achat
        shopMenu.SetActive(true);
        OpenBuyMenu();

        //on set l'instance je sais pas pourquoi
        GameManager.instance.shopActive = true;

        //on affiche les golds du joueur
        goldText.text = Player.instance.gold.ToString() + "g";

        //on stoppe le player ??? 
        Player.instance.Stop();
        Player.instance.canAttack = false;
    }

    public void CloseShop()
    {
        //on ferme les sous menus
        shopMenu.SetActive(false);
        buyMenu.SetActive(false);
        sellMenu.SetActive(false);
        //on désactive le shop ? et on réactive le joueur
        GameManager.instance.shopActive = false;
        Player.instance.canMove = true;
        Player.instance.canAttack = true;
    }

    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
        ShowBuyItems();

    }

    public void ShowBuyItems()
    {
        //pour chaque item en vente
        for (int i = 0; i < itemsForSale.Count; i++)
        {
            //on note le gameobject
            GameObject item = (GameObject)itemsForSale[i][0];
            int amount = (int)itemsForSale[i][1];

            //on active l'image et la quantité de l'objet et on note la référence de l'objet
            buyItemButtons[i].buttonImage.gameObject.SetActive(true);
            buyItemButtons[i].buttonImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            buyItemButtons[i].amountText.text = amount.ToString();
            buyItemButtons[i].setItemReferenced(item);
        }

        //pour les cases non achetables
        for (int i = itemsForSale.Count; i < buyItemButtons.Length; i++)
        {
            //on désactive le bouton, enlève les images
            buyItemButtons[i].buttonImage.gameObject.SetActive(false);
            buyItemButtons[i].amountText.text = "";
            buyItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
        }
    }

    //quand on clique sur le bouton on sélectionne l'item
    public void SelectBuyItem(GameObject buyItemSelected)
    {
        //on set le selected
        selectedItem = buyItemSelected;
        //on affiche la description et le prix
        buyItemName.text = selectedItem.GetComponent<Items>().itemName;
        buyItemDescription.text = selectedItem.GetComponent<Items>().description;
        buyItemPrice.text = "Value: " + selectedItem.GetComponent<Items>().price.ToString() +"g";
    }

    //aucune idée
    public void BuyItemCallBack()
    {
        StartCoroutine(BuyItem());
    }

    //cette histoire de coroutine c'est chelouxx
    public IEnumerator BuyItem()
    {
        //si on a sélectionné un item
        if(selectedItem != null)
        {
            //on teste si le joueur a assez de gold
            if(Player.instance.gold >= selectedItem.GetComponent<Items>().price)
            {
                if (!inventaire.isFull()) { 

                    //on ouvre une instance de popup ? (y'avait pas plus simple ???)
                    ConfirmPopUp.instance.OpenPopupConfirm("Are you sure you want to buy this item?");

                    //Tant qu'un choix n'a pas été fait sur la popup
                    while (ConfirmPopUp.instance.result == -1)
                    {
                        //on désactive les autres boutons
                        for(int i = 0; i < itemsForSale.Count; i++)
                        {
                            buyItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
                        }
                        yield return null;

                        //si on clique sur oui
                        if(ConfirmPopUp.instance.result == 1)
                        {
                            //on retire les gold du joueur du prix de l'item
                            Player.instance.gold -= selectedItem.GetComponent<Items>().price;
                            //on l'ajoute à l'inv du joueur
                            inventaire.addItem(Instantiate(selectedItem));

                            //pour chaque item en vente
                            for (int i = 0; i < itemsForSale.Count; i++)
                            {
                                //si l'item sélectionné est l'objet en vente
                                if (selectedItem == (GameObject)itemsForSale[i][0])
                                {
                                    //on baisse sa quantité
                                    int amount = (int)itemsForSale[i][1];
                                    amount--;
                                    itemsForSale[i][1] = amount;

                                    //si il passe à la quantité 0 on l'enlève
                                    if ((int)itemsForSale[i][1] <= 0)
                                    {
                                        itemsForSale.Remove(itemsForSale[i]);
                                    }

                                    break;
                                }
                            }
                            //on rafraichit l'affichage
                            ShowBuyItems();
                        } 
                        //si on clique sur non on stoppe
                        else if(ConfirmPopUp.instance.result == 0)
                        {
                            break;
                        }
                    }
                    //on rend les boutons cliquables à nouveau
                    for (int i = 0; i < itemsForSale.Count; i++)
                    {
                        buyItemButtons[i].gameObject.GetComponent<Button>().interactable = true;
                    }
                } else
                {
                    ConfirmPopUp.instance.OpenPopupSimple("Your inventory is full !");
                }
            }
            else
            {
                ConfirmPopUp.instance.OpenPopupSimple("Sorry, you do not have enough money for this item");
            }
        }

        goldText.text = Player.instance.gold.ToString() + "g";
    }

    //on change les menus
    public void OpenSellMenu()
    {
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);

        ShowSellItem();
    }


    public void ShowSellItem()
    {
        GameObject[] listItems = inventaire.listeItems;

        for (int i = 0; i < listItems.Length; i++)
        {
            GameObject item = (GameObject)listItems[i];

            if (item != null && item != Player.instance.armeCorpsACorpsEquipee && item != Player.instance.armeDistanceEquipee && item != Player.instance.armureJambesEquipee
                && item != Player.instance.armureTeteEquipee && item != Player.instance.armureTorseEquipee)
            {
                Debug.Log(item.name);
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
                sellItemButtons[i].amountText.text = "";
                sellItemButtons[i].gameObject.GetComponent<Button>().interactable = true;
                sellItemButtons[i].setItemReferenced(item);
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
                sellItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
            }
        }

        for (int i = listItems.Length; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonImage.gameObject.SetActive(false);
            sellItemButtons[i].amountText.text = "";
            sellItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void SelectSellItem(GameObject sellItemSelected)
    {
        selectedItem = sellItemSelected;
        sellItemName.text = selectedItem.GetComponent<Items>().itemName;
        sellItemDescription.text = selectedItem.GetComponent<Items>().description;
        sellItemPrice.text = "Value: " + Mathf.FloorToInt(selectedItem.GetComponent<Items>().price * 0.5f).ToString() + "g";
    }

    public void SellItemCallBack()
    {
        StartCoroutine(SellItem());
    }

    public IEnumerator SellItem()
    {
        if(selectedItem != null)
        {
            ConfirmPopUp.instance.OpenPopupConfirm("Are you sure you want to sell this item?");
            while (ConfirmPopUp.instance.result == -1)
            {
                for (int i = 0; i < inventaire.listeItems.Length; i++)
                {
                    sellItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
                }

                yield return null;


                if (ConfirmPopUp.instance.result == 1)
                {
                    Player.instance.gold += Mathf.FloorToInt(selectedItem.GetComponent<Items>().price * 0.5f);
                    inventaire.retirerItem(selectedItem);

                    ShowSellItem();
                }
                else if (ConfirmPopUp.instance.result == 0)
                {
                    break;
                }
            }

            /*
            for (int i = 0; i < inventaire.listeItems.Length; i++)
            {
                sellItemButtons[i].gameObject.GetComponent<Button>().interactable = true;
            }*/
        }

        goldText.text = Player.instance.gold.ToString() + "g";
    }

}
