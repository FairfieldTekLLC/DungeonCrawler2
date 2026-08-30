using UnityEngine;

namespace Aetherfall.Client.Data;

[CreateAssetMenu(menuName = "Aetherfall/Zones/Zone")]
public sealed class ZoneDefinition : AetherfallDefinition
{
    [SerializeField] private string[] npcIds = new string[0];
    [SerializeField] private string[] encounterIds = new string[0];

    public string[] NpcIds => npcIds;
    public string[] EncounterIds => encounterIds;
}
