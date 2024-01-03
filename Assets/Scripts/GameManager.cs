using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string finalKingName;

    public void SetFinalKingName(string name)
    {
        finalKingName = name;
    }
    
    public string GetFinalKingName() { return finalKingName; }
}
