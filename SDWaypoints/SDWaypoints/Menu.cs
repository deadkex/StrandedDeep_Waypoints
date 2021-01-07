using System;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Beam.Events;
using Beam.UI;
using Beam;

namespace SDWaypoints
{
    public class Menu : MonoBehaviour
    {
        public bool Visible = true;
        public bool wpManagerVisible = false;
        public bool hideAllWaypoints = true;
        private Rect _window;
        private Rect _window2;
        String textfieldinput = "";
        private Vector2 scrollViewVector = Vector2.zero;
        int indexNumber;
        //bool show = false;
        public List<Waypoint> waypoints = new List<Waypoint>();
        string curAssemblyFolder;

        public void Start()
        {
            //curAssemblyFolder = new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            //curAssemblyFolder = curAssemblyFolder.Split(new string[] { "common" }, StringSplitOptions.None)[0];
            //curAssemblyFolder = curAssemblyFolder + @"common/Stranded Deep/Waypoints.json";
            try
            {
                curAssemblyFolder = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%/AppData/LocalLow/Beam Team Games/Stranded Deep/Data/Waypoints.json");
                waypoints = JsonConvert.DeserializeObject<List<Waypoint>>(File.ReadAllText(curAssemblyFolder));
            }
            catch
            { }
            this._window = new Rect(10f, 10f, 250f, 200f);
            this._window2 = new Rect(262f, 10f, 250f, 150f);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                this.Visible = !this.Visible;
                this.wpManagerVisible = false;
            }
        }
        public void OnGUI()
        {
            if (this.Visible && PlayerRegistry.LocalPlayer != null)
            {
                this._window = GUILayout.Window(0, this._window, new GUI.WindowFunction(this.Draw), "SDWaypoints (F1 open/close)", new GUILayoutOption[0]);
            }
            if (this.wpManagerVisible)
            {
                this._window2 = GUILayout.Window(1, this._window2, new GUI.WindowFunction(this.wpManagerDraw), "Waypoint selector", new GUILayoutOption[0]);
            }
            if (!hideAllWaypoints && PlayerRegistry.LocalPlayer != null)
            {
                foreach (Waypoint wp in waypoints)
                {
                    if (wp.enabled)
                    {
                        Vector3 posScreen = Camera.main.WorldToScreenPoint(new Vector3(wp.x, wp.y, wp.z));
                        if (posScreen.z > 0 & posScreen.y < Screen.width - 2)
                        {
                            posScreen.y = Screen.height - (posScreen.y + 1f);
                            Render.DrawBox(new Vector2(posScreen.x, posScreen.y), new Vector2(15, 15), Color.red);
                            GUI.Label(new Rect(posScreen.x, posScreen.y - 20, 150f, 50f), wp.name);
                        }
                    }
                }
            }
        }
        public void wpManagerDraw(int id)
        {
            scrollViewVector = GUILayout.BeginScrollView(scrollViewVector, new GUILayoutOption[0]);
            Color oldcolor = GUI.color;
            for (int index = 0; index < waypoints.Count; index++)
            {
                if (waypoints[index].enabled)
                    GUI.color = Color.green;
                else
                    GUI.color = Color.red;
                if (GUILayout.Button(waypoints[index].name, new GUILayoutOption[0]))
                {
                    wpManagerVisible = false;
                    indexNumber = index;
                }
            }
            GUI.color = oldcolor;
            GUILayout.EndScrollView();
            GUI.DragWindow();
        }
        public void Draw(int id)
        {
            if(wpManagerVisible)
                GUI.enabled = false;
            hideAllWaypoints = GUILayout.Toggle(hideAllWaypoints, "Hide all waypoints", new GUILayoutOption[0]);
            textfieldinput = GUILayout.TextField(textfieldinput, 25);
            if (GUILayout.Button("New WP", new GUILayoutOption[0]))
            {
                if (textfieldinput != "")
                {
                    waypoints.Add(new Waypoint(textfieldinput, true, PlayerRegistry.LocalPlayer.transform.position));
                    textfieldinput = "";
                }
            }
            if (waypoints.Count == 0)
                GUI.enabled = false;
            if (GUILayout.Button("Delete selected WP", new GUILayoutOption[0]))
            {
                waypoints.Remove(waypoints[indexNumber]);
                wpManagerVisible = false;
                indexNumber = 0;
            }
            if (GUILayout.Button("Show/Hide selected WP", new GUILayoutOption[0]))
            {
                waypoints[indexNumber].enabled = !waypoints[indexNumber].enabled;
            }
            if (waypoints.Count == 0 || wpManagerVisible)
                GUI.enabled = true;
            

            if(!wpManagerVisible)
            {
                if(waypoints.Count == 0)
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Please add a WP first", new GUILayoutOption[0]))
                    {
                    }
                    GUI.enabled = true;
                }
                else
                {
                    Color oldcolor = GUI.color;
                    if (waypoints[indexNumber].enabled)
                        GUI.color = Color.green;
                    else
                        GUI.color = Color.red;
                    if (GUILayout.Button(waypoints[indexNumber].name, new GUILayoutOption[0]))
                    {
                        wpManagerVisible = true;
                    }
                    GUI.color = oldcolor;
                }
            }
            if (waypoints.Count == 0 || wpManagerVisible)
                GUI.enabled = false;
            if (GUILayout.Button("Save to file", new GUILayoutOption[0]))
            {
                try
                {
                    using (StreamWriter file = File.CreateText(curAssemblyFolder))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, waypoints);
                    }
                }
                catch { }
            }
            if (waypoints.Count == 0 || wpManagerVisible)
                GUI.enabled = true;
            if (GUILayout.Button("Unload Mod", new GUILayoutOption[0]))
            {
                Loader.Unload();
            }
            GUILayout.Label("Made by deadkex", new GUILayoutOption[0]);
            GUI.DragWindow();
        }
    }
}
