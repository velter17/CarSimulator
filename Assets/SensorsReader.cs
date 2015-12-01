using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SensorsReader{

    public List<KeyValuePair<Vector3, Vector3>> sensorsInfoList;

    public SensorsReader(string filePath)
    {
        sensorsInfoList = new List <KeyValuePair<Vector3, Vector3>>();
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);
        XmlNode node;
        node = doc.FirstChild.FirstChild;
        while (node != null)
        {
            if (node.Name == "sensor")
            {
                XmlNode cur = node.FirstChild;
                Vector3 position = new Vector3();
                Vector3 rotation = new Vector3();
                while (cur != null)
                {
                    if (cur.Name == "position")
                    {
                        float.TryParse(cur.Attributes.GetNamedItem("x").Value, out position.x);
                        float.TryParse(cur.Attributes.GetNamedItem("y").Value, out position.y);
                        float.TryParse(cur.Attributes.GetNamedItem("z").Value, out position.z);
                    }
                    else if (cur.Name == "rotation")
                    {
                        float.TryParse(cur.Attributes.GetNamedItem("x").Value, out rotation.x);
                        float.TryParse(cur.Attributes.GetNamedItem("y").Value, out rotation.y);
                        float.TryParse(cur.Attributes.GetNamedItem("z").Value, out rotation.z);
                    }
                    cur = cur.NextSibling;
                }
                sensorsInfoList.Add(new KeyValuePair<Vector3, Vector3>(position, rotation));
            }
            node = node.NextSibling;
        }
    }
}
