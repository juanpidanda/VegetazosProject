using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FighterPortraitBehaviour : MonoBehaviour
{
    public Image fighterPortrait;
    public TextMeshProUGUI fighterName;

    public TextMeshProUGUI lifesLeftInPortrait;
    public TextMeshProUGUI damageMultiplierText;
    public Sprite skullSprite;
    public void SetFighterPortrait(Sprite characterSprite, string playerName)
    {
        fighterPortrait.sprite = characterSprite;
        fighterName.text = "";
        fighterName.text = playerName;
        SetLifesInPortrait(3);
        SetDamageMultiplier(0);
    }

    public void SetLifesInPortrait(int lifesToSet)
    {
        lifesLeftInPortrait.text = lifesToSet.ToString();
        if(lifesToSet <= 0)
        {
            MarkDeathInPortrait();
        }
    }
    public void SetDamageMultiplier(int multiplierToSet)
    {
        damageMultiplierText.text = multiplierToSet.ToString() + "%";
    }
    void MarkDeathInPortrait()
    {
        lifesLeftInPortrait.text = "0";
        fighterPortrait.sprite = skullSprite;
    }
}
