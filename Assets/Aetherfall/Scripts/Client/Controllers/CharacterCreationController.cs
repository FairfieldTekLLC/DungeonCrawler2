using System;
using UnityEngine;

namespace Aetherfall.Client.Controllers;

public sealed class CharacterCreationController : MonoBehaviour
{
    public event Action<string, string>? CharacterCreated;

    public void Submit(string characterName, string classType)
    {
        if (string.IsNullOrWhiteSpace(characterName))
        {
            throw new ArgumentException("Character name is required.", nameof(characterName));
        }

        CharacterCreated?.Invoke(characterName.Trim(), classType);
    }
}
