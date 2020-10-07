using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Economic System")]
    public Text moneyText;
    public Text expText;
    // Start is called before the first frame updat

    // Update is called once per frame
    void Update()
    {
        moneyText.text = GameManager.instance.money.ToString();
        expText.text = GameManager.instance.exp.ToString();

    }
}
