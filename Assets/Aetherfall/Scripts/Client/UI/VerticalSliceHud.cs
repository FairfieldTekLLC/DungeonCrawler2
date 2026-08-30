using UnityEngine;

namespace Aetherfall.Client.UI;

public sealed class VerticalSliceHud : MonoBehaviour
{
    [SerializeField] private string zoneName = "Bastion Foothills";
    [SerializeField] private string activeEncounter = "Ember Vault";

    public string ZoneName => zoneName;
    public string ActiveEncounter => activeEncounter;
}
