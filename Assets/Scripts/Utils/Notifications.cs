public static class Notifications
{
    public static bool isInventoryNotificationsOn = false;

    public static void IncreaseInventory()
    {
        TurnOnNotification();
    }

    public static void DecreaseInventory()
    {
        TurnOffNotification();
    }

    public static void TurnOnNotification()
    {
        isInventoryNotificationsOn = true;
    }

    public static void TurnOffNotification()
    {
        isInventoryNotificationsOn = false;
    }
}

