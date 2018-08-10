using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevDebug : MonoBehaviour {

    public void ResterPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
