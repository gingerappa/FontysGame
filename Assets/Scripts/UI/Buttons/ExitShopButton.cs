using UnityEngine;

public class ExitShopButton : MonoBehaviour
{
    public void Exit()
    {
        ShopManager.Instance.UnLoadShop();
    }
}
