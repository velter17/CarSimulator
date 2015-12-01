using UnityEngine;
using System.Collections;
using System.Xml;

public class SensorsLogger
{
    string filePath;
    XmlDocument doc;
    XmlElement rootElement;

    public SensorsLogger(string _filePath)
    {
        filePath = _filePath;
        doc = new XmlDocument();
        rootElement = doc.CreateElement("sensors");
    }

    public void Log(Vector3 position, Vector3 rotation)
    {
        XmlNode sensor = doc.CreateElement("sensor");
        
        XmlNode node = doc.CreateElement("position");
        XmlAttribute atr = doc.CreateAttribute("x");
        atr.Value = position.x.ToString();
        node.Attributes.Append(atr);
        atr = doc.CreateAttribute("y");
        atr.Value = position.y.ToString();
        node.Attributes.Append(atr);
        atr = doc.CreateAttribute("z");
        atr.Value = position.z.ToString();
        node.Attributes.Append(atr);
        sensor.AppendChild(node);

        node = doc.CreateElement("rotation");
        atr = doc.CreateAttribute("x");
        atr.Value = rotation.x.ToString();
        node.Attributes.Append(atr);
        atr = doc.CreateAttribute("y");
        atr.Value = rotation.y.ToString();
        node.Attributes.Append(atr);
        atr = doc.CreateAttribute("z");
        atr.Value = rotation.z.ToString();
        node.Attributes.Append(atr);
        sensor.AppendChild(node);

        rootElement.AppendChild(sensor);
    }

    public void saveToFile()
    {
        Debug.Log("save to file " + filePath);
        doc.AppendChild(rootElement);
        doc.Save(filePath);
    }
}
