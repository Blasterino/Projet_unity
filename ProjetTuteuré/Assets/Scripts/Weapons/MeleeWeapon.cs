
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class MeleeWeapon : Weapon {

    public float range;
    public float hitRate;

	public void Hit(Vector2 mousePosition)
    {

        Vector3 originDirection = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
        Vector3 projectileDirection;

        projectileDirection.x = originDirection.x - player.GetComponent<Transform>().position.x;
        projectileDirection.y = originDirection.y - player.GetComponent<Transform>().position.y;
        projectileDirection.z = 0;
        projectileDirection.Normalize();
        float angle = (float)Math.Atan2(projectileDirection.y, projectileDirection.x);
        GameObject slash;
        slash = Instantiate(player.GetComponent<Player>().slashPrefab, player.transform.position, Quaternion.identity);
        slash.transform.position = new Vector3(slash.transform.position.x + ((float)Math.Cos(angle)),
            slash.transform.position.y + ((float)Math.Sin(angle)),
            slash.transform.position.z);
        angle = (float)(angle * (180 / Math.PI));
        slash.transform.localScale = new Vector3(range, range, 1);
        //Debug.Log(angle);
        
        
        //Vector3 rotat = new Vector3(0, 0, angle);
        slash.GetComponent<Transform>().Rotate(0, 0, angle);

        float rnd = UnityEngine.Random.Range(0, 100);
        if (rnd > player.gameObject.GetComponent<Player>().agility) //coup normal
        {
            slash.GetComponent<Projectile>().damage =
                    player.GetComponent<Player>().strength * this.strratio + //ajout des degats en fonction du ratio de degats force du joueur
                    player.GetComponent<Player>().agility * this.agiratio + //ajout des degats en fonction du ratio de degats agilite du joueur
                    player.GetComponent<Player>().endurance * this.endratio + //ajout des degats en fonction du ratio de degats endurance du joueur
                    this.damage;
            slash.GetComponent<Projectile>().isCrit = false;
        }
        else // coup critique
        {
            slash.GetComponent<Projectile>().damage =
                (player.GetComponent<Player>().strength * this.strratio +
                player.GetComponent<Player>().agility * this.agiratio +
                player.GetComponent<Player>().endurance * this.endratio +
                this.damage) * 2;
            slash.GetComponent<Projectile>().isCrit = true;
        }
        
        slash.GetComponent<Projectile>().isFriendly = true;
    }
    

}
