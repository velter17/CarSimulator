using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class sceneScript : MonoBehaviour {

    public GameObject cube;
    SceneContentReader sceneContent;
    SettingsReader settings;
    public GameObject carObject;
    Car car;

    SensorsLogger goodSensorLogger;
    SensorsLogger badSensorLogger;
    bool LogFlag;

    SensorsReader sensors;
    int screenId = 0;
    string savePath;

    public GUISkin recordSkin;


	// Use this for initialization
	void Start () {

        //VideoRecorder vr = new VideoRecorder();

        settings = new SettingsReader("settings.xml");

        car = carObject.GetComponent<Car>();

        car.speed = settings.velocity;
        car.cameraRotationError = settings.cameraRorationError;
        car.positionError = settings.sensorPositionError;

        sceneContent = new SceneContentReader("scene.xml");
        foreach(SceneItem item in sceneContent.itemList)
        {
            cube.transform.localScale = item.size;
            Instantiate(cube, item.position, Quaternion.Euler(item.rotation));
        }

        LogFlag = false;

        if (menuScript.mode == menuScript.CarMode.Save || menuScript.mode == menuScript.CarMode.Play)
        {
            if (menuScript.sensorsMode == menuScript.SensorsMode.GoodSensors)
                sensors = new SensorsReader(settings.goodSensorsXMLPath);
            else
                sensors = new SensorsReader(settings.badSensorsXMLPath);
            if (menuScript.mode == menuScript.CarMode.Save)
            {
                Debug.Log("read " + sensors.sensorsInfoList.Count + " sensors");
                savePath = System.IO.Directory.GetCurrentDirectory().ToString() + "\\CarSensorsFrames_" + 
                    System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
                Debug.Log(savePath);
                System.IO.Directory.CreateDirectory(savePath);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(0);

        if(menuScript.mode == menuScript.CarMode.Record)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                LogFlag = !LogFlag;
                if (LogFlag)
                {
                    goodSensorLogger = new SensorsLogger(settings.goodSensorsXMLPath);
                    badSensorLogger = new SensorsLogger(settings.badSensorsXMLPath);
                    screenId = 1;
                }
                else
                {
                    saveLog();
                }
            }

            if (LogFlag)
            {
                goodSensorLogger.Log(car.curPosition, car.curRotation);
                badSensorLogger.Log(car.curErrorPosition, car.curErrorRotation);
            }
        }
        else
        {
            if (screenId < sensors.sensorsInfoList.Count)
            {
                car.transform.position = sensors.sensorsInfoList[screenId].Key;
                car.transform.rotation = Quaternion.Euler(sensors.sensorsInfoList[screenId].Value);
                if (menuScript.mode == menuScript.CarMode.Save)
                    Application.CaptureScreenshot(savePath + "\\frame" + screenId + ".png");
                screenId++;
            }
                
        }
	}

    void OnDestroy()
    {
        if (LogFlag)
        {
            saveLog();
        }
    }

    void OnGUI()
    {
        if (menuScript.mode != menuScript.CarMode.Record && screenId == sensors.sensorsInfoList.Count)
            GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 30), "Complete!");
        if (LogFlag)
        {
            GUI.skin = recordSkin;
            GUI.Box(new Rect(10, 10, 30, 30), "", GUI.skin.GetStyle("record-circle"));
        }
    }

    void saveLog()
    {
        goodSensorLogger.saveToFile();
        badSensorLogger.saveToFile();
    }

}
