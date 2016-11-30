Thank you for purchasing our Bend Deformer script! We’ve used this to create simple wind effects and collision deformation for vegetation. Please read below for how to use the script and email us at emaildabbleduo@gmail.com with any questions!

Check out https://vimeo.com/dabbleduo/bendunity for a demo of the bend and its attributes

=====Bend Attributes

Curvature — Does the bending
Length - scales the bend gizmo
Amount — 0-1 multiplier to turn the bend on or off
Show Gizmo — Shows/hides the bend deformer
Moves — Lists the meshes the Bend will deform


=====To create a Bend object manually (see BendExample.unity)

1. Create an Empty Game Object and zero the transform
2. In the Inspector, click Add Component and choose Scripts->Bend. You should see the various attributes for the bend.
3. Expand “Moves” and set the number of objects you want to bend with this bend deformer. For example, set Size to 1. An attribute appears titled Element #.
4. Add a game object that contains a Mesh to your scene if you don’t already have one. Zero its transforms. (You can also place the Bend’s Game Object where your object is already placed.
5. Drag the game object and hover over “None (Game Object)” or select the circle icon next to “None (Game Object)” and choose a game object.
6. Use the Length attribute to scale the bend to the height of your object.
7. Use Curvature to bend the object. Hit Play to see the object deformed. 
You can hide the bend by unchecking Show Gizmo.
8. Parent the Bend under your object so that it will always be positioned where the object is.


=====To create a Bend object via script

Add the AddBend.cs script to the game object you want to deform, or use the code as an example for how you can add a bend programmatically to your project.

=====To increase performance

Reduce the number of segments in the bend in Bend.cs