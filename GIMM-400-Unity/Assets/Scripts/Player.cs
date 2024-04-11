using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI countdownText;
    
    public void SetCountDownText(string count)
    {
        countdownText.text = count;
    }
}
