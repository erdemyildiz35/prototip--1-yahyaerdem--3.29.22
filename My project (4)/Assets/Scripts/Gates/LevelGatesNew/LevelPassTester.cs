using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPassTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      Debug.Log( PlayerPrefs.GetInt("level-1-A1"));
    }

    
}
