using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerIKController))]
public class PlayerCharacter : Character
{
    [SerializeField] GameObject viewPoint;
    [SerializeField] GameObject weaponPoint;
    // Start is called before the first frame update
    // Update is called once per frame

    [SerializeField] GameObject weaponPrimary;
    [SerializeField] GameObject weaponSecondary;
    [SerializeField] GameObject weaponMelee;

}
