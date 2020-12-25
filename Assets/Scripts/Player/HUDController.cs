using UnityEngine;
public class HUDController : MonoBehaviour
{
    [SerializeField] private PlayerController _targetPlayer;
    [SerializeField] private PerformingBar _performingBar;
    [SerializeField] private GameObject _inventoryObject;
    private UITweener _inventoryTweener;

    private void Start()
    {
        _targetPlayer.OnPerformingState += ShowPerformingProgress;
        _targetPlayer.OnInventoryButton += OpenInventory;
        _inventoryTweener = _inventoryObject.GetComponent<UITweener>();
    }
    private void OpenInventory() => _inventoryObject.SetActive(true);
    public void CloseInventory() => _inventoryTweener.Disable();
    private void ShowPerformingProgress(float duration)
    {
        _performingBar.SetPerformingTime(duration);
        _performingBar.gameObject.SetActive(true);
    }
}
