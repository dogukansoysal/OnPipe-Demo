using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class CoinText : MonoBehaviour
{
    public string frontText;
    public string endText;
    private void Start()
    {
        CoinManager.Instance.CoinsChanged += SetText;
    }
    private void SetText(int value)
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = frontText+ value + endText;
    }
}
