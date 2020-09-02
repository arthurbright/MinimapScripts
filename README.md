##Topography.cs
This is a simplified script that contains the main function that generates the altitude lines on a minimap. The parameters are a TerrainData object (thus the minimap is automatically updated if the terrain is updated), and a raw image from a camera above the scene. The output is a 2D Texture that is the final minimap, and can be applied on an Image UI object.

##Collectible.cs
This script just allows the player to interact with collectible object

##Collectible.blend and Collectible Prefab
These are the sample models and prefab that I made for the collectible object. It is shaped like a hexagonal coin (just a placeholder). When the player touches a collectible, it disappears and the variable Score is incremented.
