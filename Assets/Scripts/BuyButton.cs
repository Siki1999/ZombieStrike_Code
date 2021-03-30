using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public Text Cost;
    public WeaponSwitch list;
    public Perks SecondP;

    public void BuyHealth(int amount)
    {
        if(PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost.text.Substring(0, Cost.text.Length - 1)))
        {
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost.text.Substring(0, Cost.text.Length - 1)));
            PlayerManager.instance.player.GetComponent<Player>().AddHealth(amount);
        }
    }

    public void BuyAmmo()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost.text.Substring(0, Cost.text.Length - 1)))
        {
            if(list.guns.Count > 1)
            {
                foreach(GameObject gun in list.guns)
                {
                    if(gun.activeSelf == true && list.guns[0].activeSelf == false)
                    {
                        string name = gun.name;
                        switch (name)
                        {
                            case "AK":
                                gun.GetComponent<Ak47>().AddAmmo();
                                break;
                            case "M4":
                                gun.GetComponent<M4>().AddAmmo();
                                break;
                            case "Shotgun":
                                gun.GetComponent<Shotgun>().AddAmmo();
                                break;
                            case "Awp":
                                gun.GetComponent<Awp>().AddAmmo();
                                break;
                        }
                        PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost.text.Substring(0, Cost.text.Length - 1)));
                    }
                }
            }
        }
    }

    public void BuyWeapon(GameObject Gun)
    {
        if (PlayerManager.instance.player.GetComponent<Player>().GetMoney() >= int.Parse(Cost.text.Substring(0, Cost.text.Length - 1)))
        {
            if (list.guns.Count > 1)
            {
                if(list.guns[0].activeSelf == true && SecondP.SecondPrimary)
                {
                    list.guns[0].SetActive(false);
                    list.guns[1].SetActive(true);
                    list.guns.RemoveAt(0);
                    list.guns.Add(Gun);
                }
                else
                {
                    list.guns[1].SetActive(false);
                    list.guns[0].SetActive(true);
                    list.guns.RemoveAt(1);
                    list.guns.Add(Gun);
                    list.guns[0].SetActive(false);
                    list.guns[1].SetActive(true);
                }
            }
            else
            {
                list.guns.Add(Gun);
                list.guns[0].SetActive(false);
                list.guns[1].SetActive(true);
            }
            PlayerManager.instance.player.GetComponent<Player>().RemoveMoney(int.Parse(Cost.text.Substring(0, Cost.text.Length - 1)));
        }
    }
}
