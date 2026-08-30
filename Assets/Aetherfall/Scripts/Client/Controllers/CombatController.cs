using System;
using UnityEngine;

namespace Aetherfall.Client.Controllers;

public sealed class CombatController : MonoBehaviour
{
    public event Action<string>? CombatActionRequested;

    public void RequestLightAttack() => CombatActionRequested?.Invoke("LightAttack");
    public void RequestHeavyAttack() => CombatActionRequested?.Invoke("HeavyAttack");
    public void RequestBlock() => CombatActionRequested?.Invoke("Block");
    public void RequestDodge() => CombatActionRequested?.Invoke("Dodge");
}
