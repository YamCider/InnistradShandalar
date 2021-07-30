using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardType;
    public string cardSubtype1;
    public string cardSubtype2;

    public Sprite cardArt;

    public int cmc;
    public int power;
    public int toughness;
}
