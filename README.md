Documentation of project (Overleaf): https://www.overleaf.com/read/dyfdzcmgyqdx

                  ########################### Istructions for Unity ########################

Istructions for launching on Unity

Requirements :  Unity

1)   Launch the engine
2)   Click on Open (on top-right)
3)   Select the directory "progetto-risiko-2-team2"
4)   On Project section go the folder Assets>Scenes
5)   Click the Scene : "Lobby";
6)   Click the play button (the triangle button on top)

Istruction for changing the Server IP Address and port number (from point 3 of "Istruction for launching Unity")

1)  Go to the folder Assets\scripts\server
2)  Open the ClientTcp file
3)  In the "InizializzaNetwork" method you'll find the port number and ip address of the server
4)  Edit them in order to match the desired configuration
5)  Save it
6)  Now you'll be able to create the launchable webapp, (see "Istruction for creating a webApp from Unity ")

Istruction for creating a webApp from Unity (from point 3 of "Istruction for launching on Unity", after "Istruction for changing the Server IP Address and port number ")

1)  Open the file dropdown menu and select build settings
2)  Drag and drop all scene onto the new window
3)  In the right bottom corner check WebGl application then press build and run
4)  Choose a folder and start the process
5)  After the process ended you'll find a .html file in the folder of your choice you'll be able to play on that file
