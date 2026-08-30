namespace Aetherfall.Domain.Characters;

public sealed class CharacterResources
{
    public CharacterResources(decimal health, decimal mana, decimal stamina)
    {
        Health = health;
        Mana = mana;
        Stamina = stamina;
    }

    public decimal Health { get; private set; }
    public decimal Mana { get; private set; }
    public decimal Stamina { get; private set; }

    public void SpendStamina(decimal amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (Stamina < amount) throw new InvalidOperationException("Not enough stamina.");
        Stamina -= amount;
    }

    public void SpendMana(decimal amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (Mana < amount) throw new InvalidOperationException("Not enough mana.");
        Mana -= amount;
    }

    public void ApplyDamage(decimal amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Health = Math.Max(0, Health - amount);
    }
}
