using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTab : MonoBehaviour
{
    public GameObject TO;
    public GameObject Disable1;
    public GameObject Disable2;

    public void ChangeTab()
    {
        TO.SetActive(true);
        Disable1.SetActive(false);
        Disable2.SetActive(false);
    }
}
