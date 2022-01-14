class Fighter
{
    private string fighterName { get; set; }
    private float hp { get; set; }
    private float damage { get; set; }
    private string species { get; set; }

    private int level { get; set; }

    //FIXME: This needs to be discussed as a part of the design. Does each fighter have a specific amount of variable manaSlots.
    private int manaSlots { get; set; }

    //Passive skills
    private int repeatAttackChance { get; } = 5;
    private int dodgeChance { get; } = 5;
    private int criticalChance { get; } = 5;
    private List<string> cards { get; set; }
}