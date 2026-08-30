using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Aetherfall.Client.Networking;

public sealed class AetherfallApiClient
{
    private readonly string _baseUrl;
    private string _accessToken = string.Empty;

    public AetherfallApiClient(string baseUrl)
    {
        _baseUrl = baseUrl.TrimEnd('/');
    }

    public void SetAccessToken(string accessToken) => _accessToken = accessToken ?? string.Empty;

    public async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string route, TRequest request, CancellationToken cancellationToken)
    {
        var json = JsonUtility.ToJson(request);
        using var unityRequest = new UnityWebRequest($"{_baseUrl}/{route.TrimStart('/')}", UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        unityRequest.SetRequestHeader("Content-Type", "application/json");
        if (!string.IsNullOrWhiteSpace(_accessToken))
        {
            unityRequest.SetRequestHeader("Authorization", $"******");
        }

        var operation = unityRequest.SendWebRequest();
        while (!operation.isDone)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Yield();
        }

        if (unityRequest.result != UnityWebRequest.Result.Success)
        {
            throw new InvalidOperationException(unityRequest.error + ": " + unityRequest.downloadHandler.text);
        }

        return JsonUtility.FromJson<TResponse>(unityRequest.downloadHandler.text);
    }
}
