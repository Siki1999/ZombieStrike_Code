using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkPlayerSpeed : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Text Cost1;
    public Text Cost2;
    public Text Cost3;

    public void SpeedPerkLvl1()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost1.text.Substring(0, Cost1.text.Length - 1)))
        {
            PlayerManager.instance.player.GetComponent<FPSController>().walkingSpeed += 2;
            PlayerManager.instance.player.GetComponent<FPSController>().runningSpeed += 2;
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost1.text.Substring(0, Cost1.text.Length - 1)));
            Cost1.enabled = false;
            button1.interactable = false;
            Cost2.enabled = true;
            button2.interactable = true;
        }
    }

    public void SpeedPerkLvl2()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost2.text.Substring(0, Cost2.text.Length - 1)))
        {
            PlayerManager.instance.player.GetComponent<FPSController>().walkingSpeed += 2;
            PlayerManager.instance.player.GetComponent<FPSController>().runningSpeed += 2;
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost2.text.Substring(0, Cost2.text.Length - 1)));
            Cost2.enabled = false;
            button2.interactable = false;
            Cost3.enabled = true;
            button3.interactable = true;
        }
    }

    public void SpeedPerkLvl3()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost3.text.Substring(0, Cost3.text.Length - 1)))
        {
            PlayerManager.instance.player.GetComponent<FPSController>().walkingSpeed += 2;
            PlayerManager.instance.player.GetComponent<FPSController>().runningSpeed += 2;
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost3.text.Substring(0, Cost3.text.Length - 1)));
            Cost3.enabled = false;
            button3.interactable = false;
        }
    }
}
