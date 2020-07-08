using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class MenusJeu : MonoBehaviour
    {

    private bool showPauseMenu = false;
    private bool showInventory = false;
    private bool showSaveMenu = false;
    private Player player;
    private GameObject pauseMenuCanvas, inventoryCanvas, saveCanvas, enterName1Canvas, loadCanvas, enterName2Canvas, enterName3Canvas, uiarmes, lifebar;
    private Text textSave1, textSave2, textSave3, textLoad1, textLoad2, textLoad3;
    private GameObject UIObject;

    Inputs inputs;
    float inventoryKeyPressed;
    float pauseKeyPressed;

    // Use this for initialization
    void Awake()
        {

        //inputs
        inputs = new Inputs();
        inputs.MenuControls.OpenInventory.performed += ctx => inventoryKeyPressed = ctx.ReadValue<float>();
        inputs.MenuControls.PauseMenu.performed += ctx => pauseKeyPressed = ctx.ReadValue<float>();

        //récupère le player
        player = GameObject.Find("Player").GetComponent<Player>();
        //récupère l'UI
        UIObject = GameObject.FindGameObjectWithTag("UI");
        //récupération des canvas
        pauseMenuCanvas = UIObject.transform.GetChild(0).gameObject;
        inventoryCanvas = UIObject.transform.GetChild(1).gameObject;
        enterName1Canvas = UIObject.transform.GetChild(3).gameObject;
        loadCanvas = UIObject.transform.GetChild(4).gameObject;
        saveCanvas = UIObject.transform.GetChild(2).gameObject;
        enterName2Canvas = UIObject.transform.GetChild(6).gameObject;
        enterName3Canvas = UIObject.transform.GetChild(7).gameObject;
        uiarmes = UIObject.transform.GetChild(8).gameObject;
        lifebar = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;

        //récupération des textes
        textSave1 = enterName1Canvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        textSave2 = enterName2Canvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        textSave3 = enterName3Canvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        textLoad1 = loadCanvas.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();
        textLoad2 = loadCanvas.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>();
        textLoad3 = loadCanvas.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>();

        saveCanvas.SetActive(false);
        enterName1Canvas.SetActive(false);
        loadCanvas.SetActive(false);
        enterName2Canvas.SetActive(false);
        enterName3Canvas.SetActive(false);
    }

    //used to handle key hold
    float lastInvPress;
    float lastPausePress;
    bool invKeyDown = false;
    bool pauseKeyDown = false;

    //used to handle key hold
    private void Update()
    {

        if(lastInvPress == 0 && inventoryKeyPressed == 1)
        {
            invKeyDown = true;
        }

        if (lastPausePress == 0 && pauseKeyPressed == 1)
        {
            pauseKeyDown = true;
        }
       

        lastInvPress = inventoryKeyPressed;
        lastPausePress = pauseKeyPressed;
    }

    // Update is called once per frame
    void FixedUpdate()
        {
            
            if (pauseKeyDown && showInventory == false && showSaveMenu== false && player.canOpenMenus)
            {
                showPauseMenu = !showPauseMenu;
            }

            if (invKeyDown && showPauseMenu == false && showSaveMenu==false && player.canOpenMenus)
            {
                showInventory = !showInventory;
            }

            if (showPauseMenu && !showInventory && !showSaveMenu)
            {
                pauseMenuCanvas.SetActive(true);
                uiarmes.SetActive(false);
                lifebar.SetActive(false);
                Time.timeScale = 0;
            }
            else if (!showPauseMenu && showInventory && !showSaveMenu)
            {
                uiarmes.SetActive(false);
                inventoryCanvas.SetActive(true);
            }
            else if (!showPauseMenu && !showInventory && showSaveMenu)
            {

            }
            else
            {
                uiarmes.SetActive(true);
                //On remet à jour les images après les changements dans l'inventaire, c'est un peu du cheat mais c'est normal
                GameObject UIEquip = GameObject.FindGameObjectWithTag("ArmeUI");
                UIEquip.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Image>().sprite = player.armeCorpsACorpsEquipee.GetComponent<SpriteRenderer>().sprite;
                UIEquip.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(1).gameObject.GetComponent<Image>().sprite = player.armeDistanceEquipee.GetComponent<SpriteRenderer>().sprite;
                UIEquip.GetComponent<RectTransform>().GetChild(2).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Text>().text =
                player.armeDistanceEquipee.GetComponent<RangedWeapon>().ammunition + "/" +
                player.armeDistanceEquipee.GetComponent<RangedWeapon>().totalammunition;

                lifebar.SetActive(true);
                pauseMenuCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                Time.timeScale = 1;
            }

        //reset because of update    
        invKeyDown = false;
        pauseKeyDown = false;

    }

    public void Continue()
    {
        showPauseMenu = false;
        showSaveMenu = false;
        pauseMenuCanvas.SetActive(false);
        enterName1Canvas.SetActive(false);
        loadCanvas.SetActive(false);
        enterName2Canvas.SetActive(false);
        enterName3Canvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void save()
    {
        pauseMenuCanvas.SetActive(false);
        showPauseMenu = false;
        showSaveMenu = true;
        saveCanvas.SetActive(true);
        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            if (datasnames.name != null)
            {
                textSave1.text = datasnames.name;
            }
            if (datasnames.name2 != null)
            {
                textSave2.text = datasnames.name2;
            }
            if (datasnames.name3 != null)
            {
                textSave3.text = datasnames.name3;
            }
        }
    }

    public void load()
    {
        pauseMenuCanvas.SetActive(false);
        showPauseMenu = false;
        showSaveMenu = true;
        loadCanvas.SetActive(true);
        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            if (datasnames.name != null)
            {
                textLoad1.text = datasnames.name;
            }
            if (datasnames.name2 != null)
            {
                textLoad2.text = datasnames.name2;
            }
            if (datasnames.name3 != null)
            {
                textLoad3.text = datasnames.name3;
            }
        }
    }

    public void butSaves()
    {
        saveCanvas.SetActive(false);
        enterName1Canvas.SetActive(true);
    }

    public void butSaves2()
    {
        saveCanvas.SetActive(false);
        enterName2Canvas.SetActive(true);
    }

    public void butSaves3()
    {
        saveCanvas.SetActive(false);
        enterName3Canvas.SetActive(true);
    }


    public void validate()
    {
        showSaveMenu = false;
        enterName1Canvas.SetActive(false);
        enterName2Canvas.SetActive(false);
        enterName3Canvas.SetActive(false);
    }

    public void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }

  
    public bool getShowInventory()
    {
        return this.showInventory;
    }

    public GameObject getInventoryCanvas()
    {
        return this.inventoryCanvas;
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
