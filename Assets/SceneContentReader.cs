using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public struct SceneItem
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 size;
}

public class SceneContentReader
{


    public List <SceneItem> itemList;

	public SceneContentReader(string filePath)
    {
        itemList = new List <SceneItem>();
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);
        XmlNode node;
        node = doc.FirstChild.FirstChild;
        while(node != null)
        {
            if(node.Name == "box")
            {
                XmlNode cur = node.FirstChild;
                SceneItem item = new SceneItem();
                while(cur != null)
                {
                    
                    if (cur.Name == "position")
                    {
                        float.TryParse(cur.Attributes.GetNamedItem("x").Value, out item.position.x);
                        float.TryParse(cur.Attributes.GetNamedItem("y").Value, out item.position.y);
                        float.TryParse(cur.Attributes.GetNamedItem("z").Value, out item.position.z);
                    }
                    else if (cur.Name == "rotation")
                    {
                        float.TryParse(cur.Attributes.GetNamedItem("x").Value, out item.rotation.x);
                        float.TryParse(cur.Attributes.GetNamedItem("y").Value, out item.rotation.y);
                        float.TryParse(cur.Attributes.GetNamedItem("z").Value, out item.rotation.z);
                    }
                    else if (cur.Name == "size")
                    {
                        float.TryParse(cur.Attributes.GetNamedItem("width").Value, out item.size.x);
                        float.TryParse(cur.Attributes.GetNamedItem("height").Value, out item.size.y);
                        float.TryParse(cur.Attributes.GetNamedItem("depth").Value, out item.size.z);
                    }
                    cur = cur.NextSibling;
                }
                itemList.Add(item);
            }
            
            node = node.NextSibling;
        }
    }
}
