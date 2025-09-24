using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class BuyButton : MonoBehaviour
{
    public ShopItem shopItem;
    public void Buy()
    {
        shopItem.Buy();
    }
}
