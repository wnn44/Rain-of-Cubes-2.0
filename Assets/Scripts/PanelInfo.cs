using TMPro;
using UnityEngine;

public class PanelInfo<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private GenericSpawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _totalSpawned;
    [SerializeField] private TextMeshProUGUI _totalCreated;
    [SerializeField] private TextMeshProUGUI _activeObjacts;

    private void OnEnable()
    {
        _spawner.DataChanged += UpdateView;
    }

    private void OnDisable()
    {
        _spawner.DataChanged -= UpdateView;
    }

    private void UpdateView()
    {
        _totalSpawned.text = _spawner.TotalSpawned.ToString();
        _totalCreated.text = _spawner.TotalCreated.ToString();
        _activeObjacts.text = _spawner.ActiveObjects.ToString();
    }
}
