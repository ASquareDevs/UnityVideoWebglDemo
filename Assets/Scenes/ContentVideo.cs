/**************************************************
* ContentVideo.cs
*  
**************************************************/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class ContentVideo : MonoBehaviour
{
    private WaitForSeconds waitForTenthOfASecond = new WaitForSeconds(0.1f);

    private VideoPlayer unityVideoPlayer;
    private AudioSource audioSource;

    //-----------------------------------------------------------------------------//
    public void Start()
    //-----------------------------------------------------------------------------//
    {
        CreateUnityVideoPlayer();

    } // END Start

    //---------------------------------------//
    public void CreateUnityVideoPlayer()
    //---------------------------------------//
    { 
        unityVideoPlayer = Camera.main.gameObject.AddComponent<VideoPlayer>();
        unityVideoPlayer.renderMode = VideoRenderMode.CameraNearPlane;

#if !UNITY_EDITOR && UNITY_WEBGL
       /* if (PlatformHelper.IsiOS() == true)
        {
            // Webgl on mobile IOS devices will not auto play because safari wants play to be called with user intent so from an HTML button 
            unityVideoPlayer.waitForFirstFrame = false;
            unityVideoPlayer.playOnAwake = false;
        }*/
            
        // WEBGL Requires you to use Direct audio type
        unityVideoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
#else
        audioSource = unityVideoPlayer.gameObject.AddComponent<AudioSource>();

        unityVideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        unityVideoPlayer.SetTargetAudioSource(0, audioSource);
#endif
        SetVolume(1);

        unityVideoPlayer.source = VideoSource.Url;

        unityVideoPlayer.isLooping = true;

        unityVideoPlayer.url = "https://brandxr-discovery.s3.amazonaws.com/HotspotDemoVideos/OrigamiBullHowToMakeAPaperBull!(StephanWeber).mp4";

    } //END CreateUnityVideoPlayer

    //-------------------------------------------//
    public void SetVideoUrl()
    //-------------------------------------------//
    {

#if !UNITY_EDITOR && UNITY_WEBGL

        // If we are in a mobile IOS webgl build 
      /*  if (PlatformHelper.IsiOS() == true)
        {
            IOSWebPlaybackHelper.Instance.AddToVideoList(gameObject.name);

            IOSWebPlaybackHelper.Instance.ShowHtmlButton();
        }*/
#endif

    } // END SetVideoUrl

    //-------------------------------------------//
    public void ToggleAudio()
    //-------------------------------------------//
    {
        if (IsMuted() == true)
        {

            UnMute();
        }
        else
        {
            Mute();
        }

    } // END ToggleAudio

    //-------------------------------------------//
    public bool  IsMuted()
    //-------------------------------------------//
    {

#if !UNITY_EDITOR && UNITY_WEBGL
             
        // In webgl builds is muted can be determined with GetDirectAudioMute
        return unityVideoPlayer.GetDirectAudioMute(0);
#else
        return unityVideoPlayer.GetTargetAudioSource(0).mute;
#endif
    } // END ToggleAudio

    //-------------------------------------------//
    public void Mute()
    //-------------------------------------------//
    {

#if !UNITY_EDITOR && UNITY_WEBGL

        // In webgl builds mute has to be set with direct audio mute
        unityVideoPlayer.SetDirectAudioMute(0, true);
#else
        unityVideoPlayer.GetTargetAudioSource(0).mute = true;
#endif

    } // END Mute

    //-------------------------------------------//
    public void UnMute()
    //-------------------------------------------//
    {

#if !UNITY_EDITOR && UNITY_WEBGL

        // In webgl builds unmute has to be set with direct audio mute
        unityVideoPlayer.SetDirectAudioMute(0, false);
#else
        unityVideoPlayer.GetTargetAudioSource(0).mute = false;
#endif

    } // END UnMute

    //------------------------------------------//
    public void SetVolume(float vol)
    //------------------------------------------//
    {
        if (vol > 1)
        {
            vol = 1;
        }
        else if (vol < 0)
        {
            vol = 0;
        }

#if !UNITY_EDITOR && UNITY_WEBGL

        // In webgl build video audio will have to be managed through the direct audio 
        unityVideoPlayer.SetDirectAudioVolume(0, vol);
#else
        audioSource.volume = vol;
#endif

    } //END SetVolume

} //END class

