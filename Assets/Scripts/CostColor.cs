using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostColor : MonoBehaviour
{
    [SerializeField] private List<Text> cost = new List<Text>();

    private void Update()
    {
        foreach(Text t in cost)
        {
            int costMoney = int.Parse(t.text.Substring(0, t.text.Length - 1));
            if(costMoney <= PlayerManager.instance.player.GetComponent<Player>().GetMoney())
            {
                t.color = Color.green;
            }
            else
            {
                t.color = Color.red;
            }
        }
    }
}
