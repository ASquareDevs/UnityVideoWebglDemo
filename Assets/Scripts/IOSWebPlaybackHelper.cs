/*****************************************************
* IOSWebPlaybackHelper.cs
* 
* 
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_WEBGL && !UNITY_EDITOR
using AOT;
using System.Runtime.InteropServices;
#endif

public class IOSWebPlaybackHelper : MonoBehaviour
{

#region VARIABLES
    private static IOSWebPlaybackHelper _instance;
    public static IOSWebPlaybackHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<IOSWebPlaybackHelper>();
            }

            if (_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = "IOS Web Playback Helper";

                _instance = obj.AddComponent<IOSWebPlaybackHelper>();
            }

            return _instance;
        }
    }

    public bool isWebButtonShowing = false;
    private List<string> videoPlayerList = new List<string>();
    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void HTMLVideoSolution(string objectName);                    // Calls iOSPlaybackCheck.jslib to create html button 
#else
#pragma warning disable IDE0060 // Remove unused parameter
    private void HTMLVideoSolution(string objectName) { }
#pragma warning restore IDE0060 // Remove unused parameter
#endif
    #endregion

    #region PREPARE HTML BUTTON

    //---------------------------------------//
    public void AddToVideoList(string video)
    //---------------------------------------//
    {
        // Add Videos to list so we can call play on multiple players at once
        videoPlayerList.Add(video);

    } // END AddToVideoList

    //---------------------------------------//
    public void ShowHtmlButton()
    //---------------------------------------//
    {

#if MOBILE_UI
        // Show MobileToastPopup
        MobileToastPopup.Instance.Show("TAP THE SCREEN TO START", 120f);
#endif
        // Set hmtl button showing flag to true
        isWebButtonShowing = true;
      
        // Makeing a string that will include all the names
        string names = "";

        // Make a string with all the video players seperated with commas   
        for(int i = 0; i < videoPlayerList.Count; i++) 
        {
            if (i == 0)
            {
                // First component of the array should not have a comma
                names = videoPlayerList[i];
            }
            else
            {
                // Add commas to all the others
                names = names + "," + videoPlayerList[i];
            }
        }

        // Add the name of this component as well so we call Play here and Hide the toast pop 
        names = names + "," + name;

        // Sends the names to the javascript
        HTMLVideoSolution(names);

        // Clear list for next call
        videoPlayerList.Clear();

    } // END ShowHtmlButton
#endregion

#region ON HTML BUTTON PRESSED
    //---------------------------------------//
    /// <summary>
    /// Called from iosPlaybackCheck.jslib to disable html button pop up
    /// </summary>
    public void Play()
    //---------------------------------------//
    {
        StartCoroutine(IDisableButton());

    } // END Play

    //---------------------------------------//
    /// <summary>
    /// Delays the setting of isWebButtonShowing to false because the timing is too early  
    /// </summary>
    private IEnumerator IDisableButton()
    //---------------------------------------//
    {
        yield return waitForSeconds;

#if MOBILE_UI
        MobileToastPopup.Instance.ForceHide();
#endif
        isWebButtonShowing = false;

    } // END IDisableButton
#endregion

} //END class