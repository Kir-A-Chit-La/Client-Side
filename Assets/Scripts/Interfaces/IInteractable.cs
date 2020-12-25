
public enum InteractableType
{
    Lootable,
    Simple
}

public interface IInteractable
{
    InteractableType InteractableType { get; }
    float InteractionTime { get; }
}
