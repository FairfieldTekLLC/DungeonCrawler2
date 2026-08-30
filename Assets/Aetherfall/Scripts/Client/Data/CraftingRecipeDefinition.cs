using UnityEngine;

namespace Aetherfall.Client.Data;

[System.Serializable]
public struct CraftingIngredientDefinition
{
    public string itemDefinitionId;
    public int quantity;
}

[CreateAssetMenu(menuName = "Aetherfall/Crafting/Recipe")]
public sealed class CraftingRecipeDefinition : AetherfallDefinition
{
    [SerializeField] private string profession = "Blacksmithing";
    [SerializeField] private string resultItemId = string.Empty;
    [SerializeField] private CraftingIngredientDefinition[] ingredients = new CraftingIngredientDefinition[0];

    public string Profession => profession;
    public string ResultItemId => resultItemId;
    public CraftingIngredientDefinition[] Ingredients => ingredients;
}
