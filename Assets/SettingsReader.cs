using UnityEngine;
using System.Collections;
using System.Xml;

public class SettingsReader{

    public float velocity;
    public string videoRecordPath;
    public string goodSensorsXMLPath;
    public string badSensorsXMLPath;
    public Vector3 cameraRorationError;
    public Vector3 sensorPositionError;

	public SettingsReader(string filePath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);
        XmlNode node;
        node = doc.FirstChild.FirstChild;
        while (node != null)
        {
            if (node.Name == "error")
            {
                XmlNode cur = node.FirstChild;
                while(cur != null)
                {
                    if(cur.Name == "camera")
                    {
                        XmlNode rotation = cur.FirstChild;
                        float.TryParse(rotation.Attributes.GetNamedItem("x").Value, out cameraRorationError.x);
                        float.TryParse(rotation.Attributes.GetNamedItem("y").Value, out cameraRorationError.y);
                        float.TryParse(rotation.Attributes.GetNamedItem("z").Value, out cameraRorationError.z);
                    }
                    else if(cur.Name == "sensors")
                    {
                        XmlNode position = cur.FirstChild;
                        float.TryParse(position.Attributes.GetNamedItem("x").Value, out sensorPositionError.x);
                        float.TryParse(position.Attributes.GetNamedItem("y").Value, out sensorPositionError.y);
                        float.TryParse(position.Attributes.GetNamedItem("z").Value, out sensorPositionError.z);
                    }
                    cur = cur.NextSibling;
                }
            }
            else if(node.Name == "velocity")
            {
                float.TryParse(node.Attributes.GetNamedItem("value").Value, out velocity);
            }
            else if(node.Name == "video")
            {
                videoRecordPath = node.Attributes.GetNamedItem("path").Value;
            }
            else if(node.Name == "sensors")
            {
                goodSensorsXMLPath = node.Attributes.GetNamedItem("good").Value;
                badSensorsXMLPath = node.Attributes.GetNamedItem("bad").Value;
            }
            node = node.NextSibling;
        }
    }
}
