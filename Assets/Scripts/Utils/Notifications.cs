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
        inventoryNotifications = 0;
    }

    public static void ChangeIsInventoryNotificationsOn()
    {
        isInventoryNotificationsOn = !isInventoryNotificationsOn;
    }
}

