
public enum LootType
{
    Drop = 100,
    Ore = 200,
    Stone = 300,
    Wood = 400
}

public interface ILootable : IInteractable
{
    LootType LootType { get; }
    InventoryManager TargetInventory { get; set; }
}
