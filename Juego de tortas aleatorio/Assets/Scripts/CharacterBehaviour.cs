using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    enum statsName{Attack,Dexterity,Defense,Speed};
    uint[] stats = new uint[4];

    public uint Attack;
    public uint Dexterity;
    public uint Defense;
    public uint Speed;

    string state;
    CharacterMovement Moving;
    CharacterAttack Attacking;
    CharacterTaunt Taunting;

    MonoBehaviour goLastEnabled;

    // Start is called before the first frame update
    void Start()
    {
        stats[(int)statsName.Attack] = Attack;
        stats[(int)statsName.Dexterity] = Dexterity;
        stats[(int)statsName.Defense] = Defense;
        stats[(int)statsName.Speed] = Speed;

        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i] < 1) stats[i] = 1;
            if (stats[i] > 10) stats[i] = 10;
        }

        state = "Moving"; //TODO Change to Idle when finished
        Moving = gameObject.GetComponent<CharacterMovement>();
        Attacking = gameObject.GetComponent<CharacterAttack>();
        Taunting = gameObject.GetComponent<CharacterTaunt>();

        goLastEnabled = Moving;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();

        //State machine
        switch (state)
        {
            case "Idle":
                break;
            case "Moving":
                Moving.enabled = true;
                if(goLastEnabled != Moving) goLastEnabled.enabled = false;
                goLastEnabled = Moving;
                break;
            case "LightAttack":
            case "HeavyAttack":
                Attacking.enabled = true;
                if (goLastEnabled != Attacking) goLastEnabled.enabled = false;
                goLastEnabled = Attacking;
                break;
            case "Taunting":
                Taunting.enabled = true;
                if (goLastEnabled != Taunting) goLastEnabled.enabled = false;
                goLastEnabled = Taunting;
                break;
            default:
                Moving.enabled = false;
                Attacking.enabled = false;
                Taunting.enabled = false;
                break;
        }
    }

    void ChangeState()
    {
        //Event key = Event.KeyboardEvent();

        if (Input.GetKeyDown(KeyCode.K) && state != "Taunting")
        {
            state = "HeavyAttack";
        }

        if (Input.GetKeyDown(KeyCode.J) && state != "Taunting")
        {
            state = "LightAttack";
        }

        if (Attacking.HasFinishedAttacking())
        {
            state = "Moving";
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = "Taunting";
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Taunting.StopTaunt();
            state = "Moving";
        }
    }

    public string GetState()
    {
        return state;
    }
}
