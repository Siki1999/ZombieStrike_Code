using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Perks : MonoBehaviour
{
    public Text Cost;
    public Text Cost1;
    public Text Cost2;
    public Text Cost3;
    public bool SecondPrimary;
    public Button button;
    public Button button1;
    public Button button2;
    public Button button3;
    public WeaponSwitch list;

    private void OnEnable()
    {
        foreach (GameObject gun in list.guns)
        {
            if (gun.activeSelf == true)
            {
                Cost1.enabled = true;
                button1.interactable = true;
                Cost2.enabled = true;
                button2.interactable = true;
                Cost3.enabled = true;
                button3.interactable = true;
                string name = gun.name;
                switch (name)
                {
                    case "Glock":
                        if (!gun.GetComponent<Glock>().LVL1)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<Glock>().LVL1)
                        {
                            Cost1.enabled = false;
                            button1.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<Glock>().LVL2)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = true;
                            button3.interactable = true;
                        }
                        if (gun.GetComponent<Glock>().LVL3)
                        {
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        break;
                    case "AK":
                        if (!gun.GetComponent<Ak47>().LVL1)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<Ak47>().LVL1)
                        {
                            Cost1.enabled = false;
                            button1.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<Ak47>().LVL2)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = true;
                            button3.interactable = true;
                        }
                        if (gun.GetComponent<Ak47>().LVL3)
                        {
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        break;
                    case "M4":
                        if (!gun.GetComponent<M4>().LVL1)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<M4>().LVL1)
                        {
                            Cost1.enabled = false;
                            button1.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<M4>().LVL2)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = true;
                            button3.interactable = true;
                        }
                        if (gun.GetComponent<M4>().LVL3)
                        {
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        break;
                    case "Shotgun":
                        if (!gun.GetComponent<Shotgun>().LVL1)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<Shotgun>().LVL1)
                        {
                            Cost1.enabled = false;
                            button1.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<Shotgun>().LVL2)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = true;
                            button3.interactable = true;
                        }
                        if (gun.GetComponent<Shotgun>().LVL3)
                        {
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        break;
                    case "Awp":
                        if (!gun.GetComponent<Awp>().LVL1)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<Awp>().LVL1)
                        {
                            Cost1.enabled = false;
                            button1.interactable = false;
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        if (gun.GetComponent<Awp>().LVL2)
                        {
                            Cost2.enabled = false;
                            button2.interactable = false;
                            Cost3.enabled = true;
                            button3.interactable = true;
                        }
                        if (gun.GetComponent<Awp>().LVL3)
                        {
                            Cost3.enabled = false;
                            button3.interactable = false;
                        }
                        break;
                }
            }
        }
    }

    public void SecondPrimaryPerk()
    {
        if ((PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost.text.Substring(0, Cost.text.Length - 1))) && !SecondPrimary)
        {
            SecondPrimary = true;
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost.text.Substring(0, Cost.text.Length - 1)));
            Cost.enabled = false;
            button.interactable = false;
        }
    }

    public void DamagePerkLvl1()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost1.text.Substring(0, Cost1.text.Length - 1)))
        {
            foreach (GameObject gun in list.guns)
            {
                if (gun.activeSelf == true)
                {
                    string name = gun.name;
                    switch (name)
                    {
                        case "Glock":
                            gun.GetComponent<Glock>().AddDamage(10, 40);
                            gun.GetComponent<Glock>().LVL1 = true;
                            break;
                        case "AK":
                            gun.GetComponent<Ak47>().AddDamage(12, 45);
                            gun.GetComponent<Ak47>().LVL1 = true;
                            break;
                        case "M4":
                            gun.GetComponent<M4>().AddDamage(11, 40);
                            gun.GetComponent<M4>().LVL1 = true;
                            break;
                        case "Shotgun":
                            gun.GetComponent<Shotgun>().AddDamage(40, 80);
                            gun.GetComponent<Shotgun>().LVL1 = true;
                            break;
                        case "Awp":
                            gun.GetComponent<Awp>().AddDamage(100, 160);
                            gun.GetComponent<Awp>().LVL1 = true;
                            break;
                    }
                }
            }
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost1.text.Substring(0, Cost1.text.Length - 1)));
            Cost1.enabled = false;
            button1.interactable = false;
            Cost2.enabled = true;
            button2.interactable = true;
            Cost3.enabled = false;
            button3.interactable = false;
        }
    }

    public void DamagePerkLvl2()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost2.text.Substring(0, Cost2.text.Length - 1)))
        {
            foreach (GameObject gun in list.guns)
            {
                if (gun.activeSelf == true)
                {
                    string name = gun.name;
                    switch (name)
                    {
                        case "Glock":
                            gun.GetComponent<Glock>().AddDamage(10, 40);
                            gun.GetComponent<Glock>().LVL2 = true;
                            break;
                        case "AK":
                            gun.GetComponent<Ak47>().AddDamage(12, 45);
                            gun.GetComponent<Ak47>().LVL2 = true;
                            break;
                        case "M4":
                            gun.GetComponent<M4>().AddDamage(11, 40);
                            gun.GetComponent<M4>().LVL2 = true;
                            break;
                        case "Shotgun":
                            gun.GetComponent<Shotgun>().AddDamage(40, 80);
                            gun.GetComponent<Shotgun>().LVL2 = true;
                            break;
                        case "Awp":
                            gun.GetComponent<Awp>().AddDamage(100, 160);
                            gun.GetComponent<Awp>().LVL2 = true;
                            break;
                    }
                }
            }
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost2.text.Substring(0, Cost2.text.Length - 1)));
            Cost1.enabled = false;
            button1.interactable = false;
            Cost2.enabled = false;
            button2.interactable = false;
            Cost3.enabled = true;
            button3.interactable = true;
        }
    }

    public void DamagePerkLvl3()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost3.text.Substring(0, Cost3.text.Length - 1)))
        {
            foreach (GameObject gun in list.guns)
            {
                if (gun.activeSelf == true)
                {
                    string name = gun.name;
                    switch (name)
                    {
                        case "Glock":
                            gun.GetComponent<Glock>().AddDamage(10, 40);
                            gun.GetComponent<Glock>().LVL3 = true;
                            break;
                        case "AK":
                            gun.GetComponent<Ak47>().AddDamage(12, 45);
                            gun.GetComponent<Ak47>().LVL3 = true;
                            break;
                        case "M4":
                            gun.GetComponent<M4>().AddDamage(11, 40);
                            gun.GetComponent<M4>().LVL3 = true;
                            break;
                        case "Shotgun":
                            gun.GetComponent<Shotgun>().AddDamage(40, 80);
                            gun.GetComponent<Shotgun>().LVL3 = true;
                            break;
                        case "Awp":
                            gun.GetComponent<Awp>().AddDamage(100, 160);
                            gun.GetComponent<Awp>().LVL3 = true;
                            break;
                    }
                }
            }
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost3.text.Substring(0, Cost3.text.Length - 1)));
            Cost1.enabled = false;
            button1.interactable = false;
            Cost2.enabled = false;
            button2.interactable = false;
            Cost3.enabled = false;
            button3.interactable = false;
        }
    }
}
