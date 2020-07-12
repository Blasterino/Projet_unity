using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Character {

    public static Player instance;

    //Statistiques de base du joueur
    public int basestrength = 5;
    public int baseendurance = 5;
    public int baseagility = 5;


    //Statistiques actives du joueur
    public int strength = 0;
    public int endurance = 0;
    public int agility = 0;
    public float activeSpeed = 10f;

    //attributs concernant les tirs
    float nextFire = 0.0f;

    // attributs concernant le combat
    float switchCooldown = 0.0f;

    // true -> arme de cac, false -> arme a distance
    public bool typeArmeEquipee = false;

    //Bonus/Health value
    public GameObject bonusValuePrefab;

    //dash prefab
    public GameObject slashPrefab;

    //testbool
    public bool canOpenMenus;

    //attributs concernant l'inventaire
    public GameObject armeDistanceEquipee;
    public GameObject armeCorpsACorpsEquipee;

    public GameObject armureTeteEquipee;
    public GameObject armureTorseEquipee;
    public GameObject armureJambesEquipee;

    private GameObject UIEquip;
    private Image UIPanelCAC;
    private Image UIPanelDist;

    //objets que le joueur peut ramasser
    public List<GameObject> objetsProximite;

    public InputField inputField;

    //Game over
    public GameObject gameOverText;
    private bool gameOver = false;
    public AudioSource audioSource;
    public AudioClip gameOverClip;

    public float timerStart;

    //gold
    public int gold = 0;

    public bool canMove;
    public bool canAttack;

    //classe inputs
    Inputs inputs;
    Vector2 movementInput;
    float actionKeyPressed;
    float switchKeyPressed;
    float fireKeyPressed;
    Vector2 mousePosition;


    void Awake()
    {

        //création du inputaction
        inputs = new Inputs();
        inputs.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputs.PlayerControls.ActionKey.performed += ctx => actionKeyPressed = ctx.ReadValue<float>();
        inputs.PlayerControls.SwitchWeapon.performed += ctx => switchKeyPressed = ctx.ReadValue<float>();
        inputs.PlayerControls.FireKey.performed += ctx => fireKeyPressed = ctx.ReadValue<float>();
        inputs.PlayerControls.MousePosition.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();

        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            if (datasnames.n == 1)
            {
                Datas datas = (Datas)DataManager.Load("Slot1.sav");
                if (datas != null)
                {
                    if (datas.i == 1)
                    {
                        loadDatas("Slot1.sav", 1);
                        datas.i = 0;
                        DataManager.Save(datas, "Slot1.sav");
                        datasnames.n = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
            }
            if (datasnames.n == 2)
            {
                Datas datas = (Datas)DataManager.Load("Slot2.sav");
                if (datas != null)
                {
                    if (datas.i == 1)
                    {
                        loadDatas("Slot2.sav", 2);
                        datas.i = 0;
                        DataManager.Save(datas, "Slot2.sav");
                        datasnames.n = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
            }
            if (datasnames.n == 3)
            {
                Datas datas = (Datas)DataManager.Load("Slot3.sav");
                if (datas != null)
                {
                    if (datas.i == 1)
                    {
                        loadDatas("Slot3.sav", 3);
                        datas.i = 0;
                        DataManager.Save(datas, "Slot3.sav");
                        datasnames.n = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
            }
        }
        base.Awake();
    }


    protected override void Start()
    {

        base.Start();

        instance = this;

        canMove = true;
        canAttack = true;

        objetsProximite = new List<GameObject>();
        armeDistanceEquipee.GetComponent<RangedWeapon>().equip(this.gameObject);
        armeDistanceEquipee.GetComponent<RangedWeapon>().ammunition = armeDistanceEquipee.GetComponent<RangedWeapon>().totalammunition;
        armeCorpsACorpsEquipee.GetComponent<MeleeWeapon>().equip(this.gameObject);
        lifeBar = GameObject.FindGameObjectWithTag("PlayerLifeBar").GetComponent<LifeBar>();
        lifeBar.SetProgress(currentHealth / maxHealth);

        UIEquip = GameObject.FindGameObjectWithTag("ArmeUI");
        UIPanelCAC = UIEquip.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Image>();
        UIPanelDist = UIEquip.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().GetChild(1).gameObject.GetComponent<Image>();
    }

    private void FixedUpdate() {
        if (Time.timeScale == 1)
        {
            GetInput();
        }

        if (gameOver)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (!isAlive || !canMove)
        {
            return;
        }
        Move();
    }

    
    //Affiche un texte flotant de HP rendus ou de stats gagnées
    //value : 1 : HP rendus
    // 2 : bonus force
    // 3 : bonus endu
    // 4 : bonus agi
    // 5 : ammo refill
    public void ShowBonusEffect(int value, int type)
    {
        //Vector3 rndPos = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);

        GameObject obj = Instantiate(bonusValuePrefab);

        switch (type)
        {
            case 1:
                obj.GetComponent<Text>().text = "+ "+value.ToString()+" HP";
                obj.GetComponent<Text>().color = Color.magenta;
                break;
            case 2:
                obj.GetComponent<Text>().text = "+ " + value.ToString() + " Strenght";
                obj.GetComponent<Text>().color = Color.red;
                break;
            case 3:
                obj.GetComponent<Text>().text = "+ " + value.ToString() + " Stamina";
                obj.GetComponent<Text>().color = Color.green;
                break;
            case 4:
                obj.GetComponent<Text>().text = "+ " + value.ToString() + " Agility";
                obj.GetComponent<Text>().color = Color.blue;
                break;
            case 5:
                obj.GetComponent<Text>().text = "Ammo Refill";
                obj.GetComponent<Text>().color = Color.gray;
                break;
        }
        obj.GetComponent<DestroyTimer>().EnableTimer(2.0f);
        //obj.GetComponent<MoveUpper>().moveDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 1, 0);

        obj.transform.SetParent(getCanvas().transform, false);
        obj.transform.localPosition = transform.position;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = new Vector3(0.03f, 0.03f, 1f);

    }

    private void GetInput()
    {
        //inputs directionnels
        direction = Vector2.zero;
        if (movementInput[1] >= 0.2)
        {
            direction += Vector2.up;
        }

        if (movementInput[0] <= -0.2)
        {
            direction += Vector2.left;
        }

        if (movementInput[1] <= -0.2)
        {
            direction += Vector2.down;
        }

        if (movementInput[0] >= 0.2)
        {
            direction += Vector2.right;
        }


        //input d'attaque
        if (fireKeyPressed==1 && !this.GetComponent<MenusJeu>().getShowInventory())
        {
            if(Time.time > nextFire && isAlive && canAttack)
            {
                if (typeArmeEquipee)
                {
                    animator.SetTrigger("Attacking");
                    nextFire = Time.time + armeCorpsACorpsEquipee.GetComponent<MeleeWeapon>().hitRate - 0.01f * agility;
                    armeCorpsACorpsEquipee.gameObject.GetComponent<MeleeWeapon>().Hit(mousePosition);
                } else
                {
                    nextFire = Time.time + armeDistanceEquipee.GetComponent<RangedWeapon>().fireRate - 0.01f * agility; //firerate -> cooldown de tir
                    armeDistanceEquipee.gameObject.GetComponent<RangedWeapon>().Fire(mousePosition);
                }
                    
            }
            
        }

        //input de switch d'arme
        if (switchKeyPressed == 1 && !this.GetComponent<MenusJeu>().getShowInventory())
        {
            //switch le type d'arme : cooldown de 1s
            if (Time.time > switchCooldown)
            {

                //On met en "évidence" le type d'arme équipée
                if (typeArmeEquipee)
                {
                    UIPanelCAC.enabled = false;
                    UIPanelDist.enabled = true;
                }
                else
                {
                    UIPanelCAC.enabled = true;
                    UIPanelDist.enabled = false;
                }
                switchCooldown = Time.time + 1f;
                typeArmeEquipee = !typeArmeEquipee;
            }
        }

        //input d'interaction
        if (actionKeyPressed == 1)
        {
            if(objetsProximite.Count != 0)
            {
                objetsProximite[0].GetComponent<Item>().take();
            }
        }

    
    }

    public void Move()
    {
        rigidBody.velocity = direction.normalized * activeSpeed;
    }

    public void Stop()
    {
        canMove = false;
        rigidBody.velocity = Vector2.zero;
    }

    public override void TakeDamage(float damage, Vector3 hitVector,float force, bool crit)
    {
        base.TakeDamage(damage, hitVector,force, crit);
        lifeBar.EnableLifeBar(true);
        lifeBar.SetProgress(currentHealth / maxHealth);
    }

    public override void Die()
    {
        base.Die();
        canMove = false;
        canAttack = false;

        //arrête la musique
        GameObject.Find("GameAudioManager").GetComponent<AudioSource>().Stop();

        //joue celle du gameover
        audioSource.PlayOneShot(gameOverClip);
        StartCoroutine("ShowGameOver");
    }

    private IEnumerator ShowGameOver()
    {
        for (int i = 0; i < 4; i++)
        {
            gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        gameOver = true;
    }

    public void getGold(int amount)
    {
        gold += amount;
    }

    public void loseGold(int amount)
    {
        if(amount > gold)
        {
            gold = 0;
        } else
        {
            gold -= amount;
        }

    }


    public void saveDatasf1(string guess)
    {
        saveDatas(guess, "Slot1.sav", 1);
    }

    public void saveDatasf2(string guess)
    {
        saveDatas(guess, "Slot2.sav", 2);
    }

    public void saveDatasf3(string guess)
    {
        saveDatas(guess, "Slot3.sav", 3);
    }

    public void loadDatasf1()
    {
        loadDatas("Slot1.sav", 1);
    }

    public void loadDatasf2()
    {
        loadDatas("Slot2.sav", 2);
    }

    public void loadDatasf3()
    {
        loadDatas("Slot3.sav", 3);
    }


    public void saveDatas(string guess, string filename, int numeroPartie)
    {
        if (guess.Length != 0)
        {
            DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
            if(datasnames == null)
            {
                datasnames = new DatasNames();
            }
            Datas datas = new Datas();
            if (numeroPartie == 1)
            {
                datasnames.name = guess;
                datas.name = datasnames.name;
            }
            else if (numeroPartie == 2)
            {
                datasnames.name2 = guess;
                datas.name = datasnames.name2;
            }
            else
            {
                datasnames.name3 = guess;
                datas.name = datasnames.name3;
            }
            datas.nameScene = SceneManager.GetActiveScene().name;
            datas.x = transform.position.x;
            datas.y = transform.position.y;
            datas.strength = strength;
            datas.agility = agility;
            datas.endurance = endurance;
            datas.switchCooldown = switchCooldown;
            datas.typeArmeEquipee = typeArmeEquipee;
            datas.currentHealth = currentHealth;
            datas.timerStart = timerStart;
            DataManager.Save(datas, filename);
            DataManager.Save(datasnames, "names.sav");
        }
    }


    public void loadDatas(string filename, int numeroPartie)
    {
        Datas datas = (Datas)DataManager.Load(filename);
        if (datas != null)
        {
            Vector2 position = new Vector2(datas.x, datas.y);
            this.transform.position = position;
            strength = datas.strength;
            agility = datas.agility;
            endurance = datas.endurance;
            switchCooldown = datas.switchCooldown;
            typeArmeEquipee = datas.typeArmeEquipee;
            currentHealth = datas.currentHealth;
            timerStart = datas.timerStart;
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
