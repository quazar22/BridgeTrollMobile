using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizer : MonoBehaviour {

    RectTransform ip;
    RectTransform port;
    RectTransform hostbutton;
    RectTransform playbutton;
    // Use this for initialization
    void Start ()
    {
        RectTransform canvas = GetComponent<RectTransform>();
        ip = GameObject.Find("IP").GetComponent<RectTransform>();
        port = GameObject.Find("Port").GetComponent<RectTransform>();
        hostbutton = GameObject.Find("HostButton").GetComponent<RectTransform>();
        playbutton = GameObject.Find("PlayButton").GetComponent<RectTransform>();


        ip.sizeDelta = new Vector2(canvas.rect.width * 0.32f, canvas.rect.height * 0.11f);
        port.sizeDelta = new Vector2(canvas.rect.width * 0.14f, canvas.rect.height * 0.11f);
        hostbutton.sizeDelta = new Vector2(canvas.rect.width * 0.19f, canvas.rect.height * 0.16f);
        playbutton.sizeDelta = new Vector2(canvas.rect.width * 0.19f, canvas.rect.height * 0.16f);


        ip.localPosition = new Vector2(-ip.rect.width / 2, 0);
        port.localPosition = new Vector2(port.rect.width, 0);
        hostbutton.localPosition = new Vector2((-canvas.rect.width / 2) + playbutton.rect.width * 0.70f,
                                               (-canvas.rect.height / 2) + playbutton.rect.height * 0.80f);
        playbutton.localPosition = new Vector2((canvas.rect.width / 2) - playbutton.rect.width * 0.70f,
                                               (-canvas.rect.height / 2) + playbutton.rect.height * 0.80f);
        foreach (Text t in GameObject.Find("Canvas").GetComponentsInChildren<Text>())
        {
            t.fontSize = (int)(playbutton.rect.height * 0.4f);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
