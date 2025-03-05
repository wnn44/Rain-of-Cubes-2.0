using TMPro;
using UnityEngine;

public class PanelInfo : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpavner;
    [SerializeField] private TextMeshProUGUI _totalSpawnedCube;
    [SerializeField] private TextMeshProUGUI _totalCreatedCube;
    [SerializeField] private TextMeshProUGUI _activeObjactsCube;

    [SerializeField] private BombSpawner _bombSpavner;
    [SerializeField] private TextMeshProUGUI _totalSpawnedBomb;
    [SerializeField] private TextMeshProUGUI _totalCreatedBomb;
    [SerializeField] private TextMeshProUGUI _activeObjactsBomb;

    private void LateUpdate()
    {
        _totalSpawnedCube.text = _cubeSpavner.TotalSpawned.ToString();
        _totalCreatedCube.text = _cubeSpavner.TotalCreated.ToString();
        _activeObjactsCube.text = _cubeSpavner.ActiveObjacts.ToString();

        _totalSpawnedBomb.text = _bombSpavner.TotalSpawned.ToString();
        _totalCreatedBomb.text = _bombSpavner.TotalCreated.ToString();
        _activeObjactsBomb.text = _bombSpavner.ActiveObjacts.ToString();
    }
}
