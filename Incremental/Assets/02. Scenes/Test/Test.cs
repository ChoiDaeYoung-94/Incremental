using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MiniJSON;

public class Test : MonoBehaviour
{
    public GameObject TestObj;

    void Start()
    {

    }

    private void Update()
    {

    }

    void TransformTest()
    {
        Vector2 test = Camera.main.WorldToScreenPoint(transform.position);
        TestObj.GetComponent<RectTransform>().anchoredPosition = test;
        Debug.Log(test);
    }

    void JsonTest()
    {
        Debug.Log(Application.dataPath);
        var jsonString = "{ \"array\": [1.44,2,3], " +
                        "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
                        "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
                        "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
                        "\"int\": 65536, " +
                        "\"float\": 3.1415926, " +
                        "\"bool\": true, " +
                        "\"null\": null }";

        var dict = Json.Deserialize(jsonString) as Dictionary<string, object>;

        Debug.Log("deserialized: " + dict.GetType());
        Debug.Log("dict['array'][0]: " + ((List<object>)dict["array"])[0]);
        Debug.Log("dict['string']: " + (string)dict["string"]);
        Debug.Log("dict['float']: " + (double)dict["float"]); // floats come out as doubles
        Debug.Log("dict['int']: " + (long)dict["int"]); // ints come out as longs
        Debug.Log("dict['unicode']: " + (string)dict["unicode"]);

        var str = Json.Serialize(dict);

        Debug.Log("serialized: " + str);
    }
}
