public static class Notifications
{
    static int inventoryNotifications = 0;
    public static bool isInventoryNotificationsOn = false;

    public static void IncreaseInventory()
    {
        inventoryNotifications++;
    }

    public static void DecreaseInventory()
    {
        if(inventoryNotifications >= 0)
            inventoryNotifications--;
    }

    public static void ChangeIsInventoryNotificationsOn()
    {
        isInventoryNotificationsOn = !isInventoryNotificationsOn;
    }
}

