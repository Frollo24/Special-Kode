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

    private void Update()
    {
        //Debug.Log(StunTime);
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
            fDamage += Random.Range(0.01f, 0.13f);
            Throwing.AddDamage(fDamage);
            fStunTimer += Time.deltaTime;
            Debug.Log(fStunTimer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        fStunTimer = 0.0f;
        other.gameObject.GetComponent<Rigidbody2D>().AddForce(7.0f * Throwing.GetDamage() * (Vector2.right + 0.5f * Vector2.up));
        Debug.Log("Exit the collision");
    }
}
