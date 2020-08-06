using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Locating : MonoBehaviour
{
	[SerializeField] Text textState;
	[SerializeField] Text textContinent;
	[SerializeField] Text textCountry;
	[SerializeField] Text textCity;

	void Start ()
	{
		textState.text = "";
		StartCoroutine ("DetectCountry");
	}

	IEnumerator DetectCountry ()
	{
		UnityWebRequest request = UnityWebRequest.Get ("https://extreme-ip-lookup.com/json");
		request.chunkedTransfer = false;
		yield return request.Send ();
		textState.text = "Locating...";

		if (request.isError) {
			textState.text = "error : " + request.error;
		} else {
			if (request.isDone) {
				Country res = JsonUtility.FromJson<Country> (request.downloadHandler.text);
				textState.text = "";
				textContinent.text = res.continent;
				textCity.text = res.city;
				textCountry.text = res.country;
			}
		}
	}
}
