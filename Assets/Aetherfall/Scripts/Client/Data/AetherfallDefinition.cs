using UnityEngine;

namespace Aetherfall.Client.Data;

public abstract class AetherfallDefinition : ScriptableObject
{
    [SerializeField] private string id = string.Empty;
    [SerializeField] private string displayName = string.Empty;
    [SerializeField, TextArea] private string description = string.Empty;

    public string Id => id;
    public string DisplayName => displayName;
    public string Description => description;
}
