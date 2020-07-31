using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class HitboxThrow : MonoBehaviour
{

    float fDamage;
    float StunTime;
    float fStunTimer;

    CharacterThrow Throwing;

    // Start is called before the first frame update
    void Start()
    {
        fDamage = 0.0f;
        fStunTimer = 0.0f;

        Throwing = null;
    }

    void Update()
    {
        //Debug.Log(StunTime);
    }

    float CalculateThrowingDistance(float damageInput)
    {
        return 10 * Mathf.Sqrt(damageInput/0.054f) / 0.64f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StunTime = gameObject.GetComponentInParent<CharacterAttack>().GetFinishTime();
        Throwing = other.gameObject.GetComponent<CharacterThrow>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Throwing != null)
        {
            fDamage += UnityEngine.Random.Range(0.01f, 0.13f);
            Throwing.AddDamage(fDamage);
            fStunTimer += Time.deltaTime;
            //Debug.Log(fStunTimer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        fStunTimer = 0.0f;
        if (Throwing != null)
        {
            other.gameObject.transform.position += 0.2f * Vector3.up;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(CalculateThrowingDistance(Throwing.GetDamage()) * Mathf.Sqrt(0.5f) * (Vector2.right + 0.2f * Vector2.up));
        }
        Debug.Log("Exit the collision");
    }
}
