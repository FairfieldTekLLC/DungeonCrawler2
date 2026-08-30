using UnityEngine;
using Aetherfall.Client.Networking;

namespace Aetherfall.Client.Bootstrap;

public sealed class AetherfallBootstrap : MonoBehaviour
{
    [SerializeField] private string apiBaseUrl = "https://localhost:5001/api";

    public AetherfallApiClient? ApiClient { get; private set; }

    private void Awake()
    {
        ApiClient = new AetherfallApiClient(apiBaseUrl);
    }
}
