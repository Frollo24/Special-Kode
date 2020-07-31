using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterThrow : MonoBehaviour
{
    // Start is called before the first frame update
    float fDamage;
    SpriteRenderer rend;
    void Start()
    {
        fDamage = 10.0f;
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetDamage());
        rend.color = new Color(1 - 0.05f * (-130 + fDamage), 1 - 0.05f * (-60 + fDamage), 1 - (0.05f * fDamage), 1);
    }

    public float GetDamage()
    {
        return fDamage;
    }

    public void AddDamage(float newDamage)
    {
        fDamage += newDamage;
    }
}
