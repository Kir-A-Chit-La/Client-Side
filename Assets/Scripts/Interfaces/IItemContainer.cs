
public interface IItemContainer
{
    int ItemCount(string itemId);
    //bool ContainsItem(Item item);
    bool AddItem(Item item);
    Item RemoveItem(string itemId);
    bool RemoveItem(Item item);
    bool CanAddItem(Item item, int amount = 1);
    void Clear();
}
