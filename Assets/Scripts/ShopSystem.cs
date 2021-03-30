using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject Shop;
    public Player player;
    public FPSController playerMovement;
    public Text money;

    public bool ShopOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShopOpen = !ShopOpen;
            Shop.SetActive(ShopOpen);
            player.dead = ShopOpen;
            playerMovement.enabled = !ShopOpen;
            if (ShopOpen)
            {
                money.text = player.GetMoney().ToString() + "$";
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Cursor.visible = ShopOpen;
        }
        if(int.Parse(money.text.Substring(0, money.text.Length - 1)) != player.GetMoney())
        {
            money.text = player.GetMoney().ToString() + "$";
        }
    }
}
