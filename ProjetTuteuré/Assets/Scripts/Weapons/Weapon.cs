﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public int damage;

    public void equip(GameObject player)
    {
        this.player = player;
    }
}
