using UnityEngine;

namespace Aetherfall.Client.Data;

[CreateAssetMenu(menuName = "Aetherfall/Companion/Companion")]
public sealed class CompanionDefinition : AetherfallDefinition
{
    [SerializeField] private string companionClass = "Mage";
    [SerializeField] private int loyalty = 50;
    [SerializeField] private int courage = 50;
    [SerializeField] private int wisdom = 50;

    public string CompanionClass => companionClass;
    public int Loyalty => loyalty;
    public int Courage => courage;
    public int Wisdom => wisdom;
}
