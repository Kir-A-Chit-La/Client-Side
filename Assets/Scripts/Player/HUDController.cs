using UnityEngine;
public class HUDController : MonoBehaviour
{
    [SerializeField] private PlayerController _targetPlayer;
    [SerializeField] private PerformingBar _performingBar;
    [SerializeField] private GameObject _inventoryObject;
    [SerializeField] private GameObject _characterPanelObject;
    [SerializeField] private GameObject _craftingPanelObject;
    private UITweener _inventoryTweener;
    private UITweener _characterPanelTweener;
    private UITweener _craftingPanelTweener;

    private void Start()
    {
        _targetPlayer.OnPerformingState += ShowPerformingProgress;
        _targetPlayer.OnInventoryButton += OpenInventory;
        _inventoryTweener = _inventoryObject.GetComponent<UITweener>();
        _characterPanelTweener = _characterPanelObject.GetComponent<UITweener>();
        _craftingPanelTweener = _craftingPanelObject.GetComponent<UITweener>();
    }
    private void OpenInventory() => _inventoryObject.SetActive(true);
    public void CloseInventory() => _inventoryTweener.Disable();
    public void OpenCrafringWindow()
    {
        if(!_craftingPanelObject.activeSelf)
        {
            _characterPanelTweener.Disable();
            _craftingPanelObject.SetActive(true);   
        }
        else
        {
            _craftingPanelTweener.Disable();
            _characterPanelObject.SetActive(true);
        }
    }
    private void ShowPerformingProgress(float duration)
    {
        _performingBar.SetPerformingTime(duration);
        _performingBar.gameObject.SetActive(true);
    }
}
