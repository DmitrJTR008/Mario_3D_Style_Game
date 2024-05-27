using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
   
    public void HandleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
