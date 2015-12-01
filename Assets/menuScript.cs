using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {

    public enum CarMode
    {
        Play,
        Record,
        Save,
    };

    public enum SensorsMode
    {
        GoodSensors,
        BadSensors,
    }

    public static CarMode mode;
    public static SensorsMode sensorsMode;
    int menuId = 0;

    void OnGUI()
    {
        if (menuId == 0)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 220), "Menu");
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 40, 180, 30), "Play"))
            {
                mode = CarMode.Play;
                menuId = 1;
                //Application.LoadLevel(1);
            }
            else if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 80, 180, 30), "Record"))
            {
                mode = CarMode.Record;
                Application.LoadLevel(1);
            }
            else if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2, 180, 30), "Save"))
            {
                mode = CarMode.Save;
                //Application.LoadLevel(1);
                menuId = 1;
            }
            else if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 80, 180, 30), "Exit"))
            {
                Application.Quit();
            }
        }
        else 
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 220), "Sensors");
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 80, 180, 30), "Good"))
            {
                sensorsMode = SensorsMode.GoodSensors;
                Application.LoadLevel(1);
            }
            else if(GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 40, 180, 30), "Bad"))
            {
                sensorsMode = SensorsMode.BadSensors;
                Application.LoadLevel(1);
            }
            else if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 80, 180, 30), "Back"))
            {
                menuId = 0;
            }
        }
    }
}
