using UnityEngine;
using UnityEngine.UI;

public class Counter<TObject> : MonoBehaviour where TObject : MonoBehaviour, IObject<TObject>
{
    [SerializeField] private Spawner<TObject> _spawner;

    private Text _text;

    private void Awake() =>    
        _text = GetComponent<Text>();

    private void Update()
    {
        _text.text = $"{typeof(TObject).Name}s: \n" +
                     $"Spawned all time: {_spawner.CreatedAllTime}\n" +
                     $"Created: {_spawner.CountAll}\n" +
                     $"Active amount: {_spawner.CountActive}";
    }
}