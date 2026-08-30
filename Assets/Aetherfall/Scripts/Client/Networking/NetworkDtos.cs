using System;

namespace Aetherfall.Client.Networking;

[Serializable]
public sealed class AuthResponseDto
{
    public string accountId = string.Empty;
    public string email = string.Empty;
    public string accessToken = string.Empty;
}

[Serializable]
public sealed class CharacterSummaryDto
{
    public string characterId = string.Empty;
    public string name = string.Empty;
    public string classType = string.Empty;
    public int level;
    public float maxHealth;
    public float maxMana;
    public float maxStamina;
}
