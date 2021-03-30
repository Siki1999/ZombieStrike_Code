using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] public List<GameObject> guns = new List<GameObject>();
    public ShopSystem shop;

    private int gunId = 0;
    // Start is called before the first frame update
    void Start()
    {
        guns[gunId].SetActive(true);
        for(int i = 1; i<guns.Count; i++)
        {
            guns[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(guns.Count > 1 && !shop.ShopOpen)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                guns[gunId].SetActive(false);
                gunId++;
                if (gunId >= guns.Count)
                {
                    gunId = 0;
                }
                guns[gunId].SetActive(true);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                guns[gunId].SetActive(false);
                if (gunId == 0)
                {
                    gunId = guns.Count - 1;
                }
                else
                {
                    gunId--;
                }
                guns[gunId].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                guns[1].SetActive(false);
                guns[0].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                guns[1].SetActive(true);
                guns[0].SetActive(false);
            }
        }
    }
}
