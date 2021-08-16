mergeInto(LibraryManager.library, 
{
	$ContentVideo: {},
	
//Open a url in a different tab
OpenUrl: function(url)
{
	window.open(Pointer_stringify(url), "_blank");
	
},//END OpenUrl

//HTML button plugin for WEBGL iOS
HTMLVideoSolution: function(objectNames) 
  {	  		
	alert("Tap the screen to play the video");
	
	// Take the object names and split it into a list
	var videoPlayerNames = Pointer_stringify( objectNames );	
	var videoPlayerArray = videoPlayerNames.split(',');
	
	//Create a button that will call a function in unity	
	var vidButton = document.createElement('button');
	vidButton.id = 'Test Button';
	vidButton.style.left = '-50%';
	vidButton.style.height = '200%';
	vidButton.style.width = '200%';
	//vidButton.style.zIndex = '1010';
	vidButton.style.position = 'absolute';
	vidButton.style.opacity = '.45';
	vidButton.style.top = '-50%';
	
	//Add Button Listener
	vidButton.onclick = function()
	{		
		if(videoPlayerArray.length > 0)
		{
			for(var i = 0; i < videoPlayerArray.length; i++)
			{
				// Call Unity function for all video players in array
				window.unityInstance.SendMessage( videoPlayerArray[i], 'Play' );
			}
		}
		
		vidButton.remove();
	}

	document.body.appendChild(vidButton);	

  } //END HTMLVideoSolution

});