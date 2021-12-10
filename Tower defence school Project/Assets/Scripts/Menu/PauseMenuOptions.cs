using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOptions : MonoBehaviour
{

    public void Options()
    {
        GameObject.Find("UiCanvas").GetComponent<PauseMenu>().enabled = false;
    }

    public void PauseMenuBack()
    {
        GameObject.Find("UiCanvas").GetComponent<PauseMenu>().enabled = true;
    }

}
