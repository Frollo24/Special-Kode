﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    uint iAttack = 7;

    float timer = 0.0f;
    float finishTime;

    char cAttackType = 'N';

    public GameObject Hitbox;
    GameObject goHitbox;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        if (gameObject.GetComponent<CharacterBehaviour>() != null)
            iAttack = gameObject.GetComponent<CharacterBehaviour>().Attack;

        goHitbox = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SetTypeOfAttack();
        AttackTimer();
        //Debug.Log("I'm attacking right now");
        /**
        if(goHitbox == null)
        {
            if (cAttackType == 'N')
                goHitbox = Instantiate(Hitbox, (Vector2)transform.position, Quaternion.identity);
            if (cAttackType == 'U')
                goHitbox = Instantiate(Hitbox, (Vector2)transform.position + 2 * Vector2.up, Quaternion.identity);
            if (cAttackType == 'D')
                goHitbox = Instantiate(Hitbox, (Vector2)transform.position + 2 * Vector2.down, Quaternion.identity);
            if (cAttackType == 'L')
                goHitbox = Instantiate(Hitbox, (Vector2)transform.position + 2 * Vector2.left, Quaternion.identity);
            if (cAttackType == 'R')
                goHitbox = Instantiate(Hitbox, (Vector2)transform.position + 2 * Vector2.right, Quaternion.identity);
        }
        //*/
        if(goHitbox.activeInHierarchy == false)
        {
            goHitbox.SetActive(true);

            switch (cAttackType)
            {
                case 'N':
                    goHitbox.transform.position = (Vector2)transform.position + 2 * Vector2.zero;
                    break;
                case 'U':
                    goHitbox.transform.position = (Vector2)transform.position + 2 * Vector2.up;
                    break;
                case 'D':
                    goHitbox.transform.position = (Vector2)transform.position + 2 * Vector2.down;
                    break;
                case 'L':
                    goHitbox.transform.position = (Vector2)transform.position + 2 * Vector2.left;
                    break;
                case 'R':
                    goHitbox.transform.position = (Vector2)transform.position + 2 * Vector2.right;
                    break;
            }
        }

            
        if (gameObject.GetComponent<CharacterBehaviour>().GetState() == "LightAttack")
            finishTime = 0.2f;
        else
            finishTime = 0.8f;
    }

    char SetTypeOfAttack()
    {
        cAttackType = 'N';
        if (Input.GetKey(KeyCode.W)) cAttackType = 'U';
        if (Input.GetKey(KeyCode.A)) cAttackType = 'L';
        if (Input.GetKey(KeyCode.S)) cAttackType = 'D';
        if (Input.GetKey(KeyCode.D)) cAttackType = 'R';
        return cAttackType;
    }

    void AttackTimer()
    {
        timer += Time.deltaTime;
    }

    public bool HasFinishedAttacking()
    {
        if(timer > finishTime)
        {
            //Destroy(goHitbox);
            goHitbox.SetActive(false);
            timer = 0.0f;
            return true;
        }
        return false;
    }

    public float GetFinishTime()
    {
        return finishTime;
    }
}
