using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{

    private bool canOpen;

    public GameObject[] itemsInStock = new GameObject[24];
    public List<object[]> itemsForSale;
    private MenusJeu menusJeu;

    Inputs inputs;
    float shopKeyPressed;

    private void Awake()
    {
        //inputs
        inputs = new Inputs();
        inputs.MenuControls.OpenShop.performed += ctx => shopKeyPressed = ctx.ReadValue<float>();

        //menujeu
        menusJeu = GameObject.FindGameObjectWithTag("Player").GetComponent<MenusJeu>();
    }

    // Start is called before the first frame update
    void Start()
    {
        itemsForSale = new List<object[]>();

        selectItemsToSale();
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpen && shopKeyPressed==1 && !Shop.instance.shopMenu.activeInHierarchy && !menusJeu.getPauseMenuOpened())
        {
            Shop.instance.itemsForSale = itemsForSale;
            Shop.instance.OpenShop();
            menusJeu.setShopMenu(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = false;
        }
    }

    private void selectItemsToSale()
    {
        for(int i = 0; i < itemsInStock.Length; i++)
        {
            if(itemsInStock[i] != null)
            {
                int prob = Random.Range(0, 100);
                if(prob >= 50)
                {
                    itemsForSale.Add(new object[]{itemsInStock[i], Random.Range(1, 3)});
                }
            }
        }
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
