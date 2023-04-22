using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using System;

public class WebRequestBuilder : IDisposable
{

    private string url;
    private string type;
    private bool revocable = false;
    private bool JsonContentType = false;
    private object data;
    private UploadHandler uploadHandler;
    private DownloadHandler downloadHandler;

    private UnityWebRequest request;

    /* public WebRequestBuilder(UnityWebRequest request)
     {
         this.request = request;
     } */

    public WebRequestBuilder SetUrl(string url)
    {
        this.url = $"https://parseapi.back4app.com/{url}";
        return this;
    }

    public WebRequestBuilder SetType(string type)
    {
        this.type = type;
        return this;
    }

    public WebRequestBuilder Revocable()
    {
        revocable = true;
        return this;
    }

    public WebRequestBuilder JSONContentType()
    {
        JsonContentType = true;
        return this;
    }

    public WebRequestBuilder SetJSON(object data)
    {
        this.data = data;
        uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        return this;
    }

    public WebRequestBuilder SetDownloadHandler(DownloadHandler downloadHandler)
    {
        this.downloadHandler = downloadHandler;
        return this;
    }

    public UnityWebRequest Build()
    {
        var request = new UnityWebRequest();
        if (type == "POST")
        {
            request = new UnityWebRequest(url, type);
        }
        else if (type == "GET")
        {
            request = UnityWebRequest.Get(url);
        }
        request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
        request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);

        if (revocable)
        {
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
        }
        if (JsonContentType)
        {
            request.SetRequestHeader("Content-Type", "application/json");
        }
        if (data != null)
        {
            request.uploadHandler = uploadHandler;
        }
        if (downloadHandler != null)
        {
            request.downloadHandler = downloadHandler;
        }





        return request;
    }


    public void Dispose()
    {
        request.Dispose();
    }
}
