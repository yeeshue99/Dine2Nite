using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequests : MonoBehaviour
{
    readonly string t_CLIENTID = "CLIENTID";
    readonly string t_CLIENTSECRET = "CLIENTSECRET";
    readonly string t_ORDERID = "12345";
    readonly string t_ACCOUNTID = "ACCOUNTID";

    readonly string GETLINKEDACCOUNTS = "https://api.staging.deliverect.com/accounts";
    readonly string GETLOCATIONS = "https://api.staging.deliverect.com/locations?where={\"account\":\"\"}";
    readonly string GETNEWORDERS = "https://api.staging.deliverect.com/my-orders?where={\"account\":\"\",\"location\":\"{{locationId}}\",\"status \":2,\"_updated \": {\"$gte \": \"2019 - 02 - 20T00: 00:00.000Z \"}}&sort=-_created";

    readonly string POSTACCESSTOKEN = "https://api.staging.deliverect.com/oauth/token";
    readonly string POSTUPDATEORDER = "https://api.staging.deliverect.com/orderStatus/5d08c4e3448c290010c84453";

    public TextMeshProUGUI messageBox;

    #region External Trigger Hooks

    public void OnButtonGetLinkedAccounts()
    {
        messageBox.text = "Downloading Linked Accounts...";
        StartCoroutine(GetRequest(GETLINKEDACCOUNTS));
    }

    public void OnButtonPostAccessToken()
    {
        messageBox.text = "Setting public access token...";

        WWWForm form = new WWWForm();
        form.AddField("client_id", t_CLIENTID);
        form.AddField("client_secret", t_CLIENTSECRET);
        form.AddField("audience", "https://api.deliverect.com");
        form.AddField("grant_type", "client_credentials");

        StartCoroutine(PostRequest(POSTACCESSTOKEN, form));
    }

    public void OnButtonGetLocations()
    {
        messageBox.text = "Downloading Locations...";
        StartCoroutine(GetRequest(GETLOCATIONS));
    }

    public void OnButtonGetNewOrders()
    {
        messageBox.text = "Downloading New Orders...";
        StartCoroutine(GetRequest(GETNEWORDERS));
    }

    public void OnButtonUpdateOrderStatus()
    {
        messageBox.text = "Updating Order Status...";

        WWWForm form = new WWWForm();
        form.AddField("orderId", t_ORDERID);
        form.AddField("status", $"{OrderStatus.READYFORPICKUP}");
        form.AddField("timeStamp", $"{System.DateTime.Now}");
        form.AddField("receiptId", "1234");

        StartCoroutine(PostRequest(POSTUPDATEORDER, form));
    }

    public void OnButtonRetryOrder()
    {
        messageBox.text = "Retrying Order...";
        StartCoroutine(GetRequest($"https://api.staging.deliverect.com/retry/{t_ORDERID}"));
    }

    public void OnButtonUpdateOrderPreperationTime()
    {
        messageBox.text = "Downloading New Orders...";

        WWWForm form = new WWWForm();
        form.AddField("order", t_ORDERID);
        form.AddField("minutes", 15);

        StartCoroutine(PostRequest($"https://api.staging.deliverect.com/updatePreparationTime/{t_ORDERID}", form));
    }

    public void OnButtonCreateDeliveryJob()
    {
        messageBox.text = "Downloading New Orders...";

        WWWForm form = new WWWForm();
        form.AddField("jobId", t_ORDERID);
        form.AddField("account", t_ACCOUNTID);

        StartCoroutine(GetRequest(GETNEWORDERS));
    }

    public void OnButtonUpdateDeliveryJob()
    {
        messageBox.text = "Downloading New Orders...";
        StartCoroutine(GetRequest(GETNEWORDERS));
    }

    public void OnButtonCancelDeliveryJob()
    {
        messageBox.text = "Downloading New Orders...";
        StartCoroutine(GetRequest(GETNEWORDERS));
    }

    #endregion

    #region HTTP Requests

    private IEnumerator GetRequest(string getURL)
    {
        UnityWebRequest www = UnityWebRequest.Get(getURL);

        yield return www.SendWebRequest();

        wwwHandler(www);
    }

    private IEnumerator PostRequest(string postURL, WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(POSTACCESSTOKEN, form);

        yield return www.SendWebRequest();

        wwwHandler(www);
    }

    #endregion

    #region Helpers
    private void Start()
    {
        messageBox.text = "Welcome to Dine2Nite!";
    }

    private void wwwHandler(UnityWebRequest www)
    {
        try
        {
            if (www.isNetworkError || www.isHttpError)
            {
                messageBox.text = www.error;
                Debug.Log(www.error);
            }
            else
            {
                messageBox.text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
            }
            www.Dispose();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    enum OrderStatus
    {
        PARSED = 1,
        RECEIVED = 2,
        NEW = 10,
        ACCEPTED = 20,
        PREPARING = 50,
        READYFORPICKUP = 70,
        INDELIVERY = 80,
        FINALIZED = 90
    };
    #endregion
}
