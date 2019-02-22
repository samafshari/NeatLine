# NeatLine
2D Line Library for Unity

## Installing
Just copy the `Assets/NeatLine` folder to your project's `Assets`.

## Basic Lines (`NeatLine`)

### Creating a NeatLine
Use the GameObject>2D Object>NeatLine menu item to create a NeatLine instance.

### Parameters
NeatLine provides the following parameters:
- `Vector2 HeadLocalPosition`: Position of the first point, relative to the local transform
- `Vector2 TailLocalPosition`: Position of the second point, relative to the local transform
- `float Thickness`: Line thickness
- `Color Color`: Line color

## Advanced Lines (`NeatPolyline`)

NeatPolyline supports more than one vertex, as well as independent thickness and color settings for each vertex.

### Creating a NeatPolyline
Use the GameObject>2D Object>NeatPolyline menu item to create a NeatPolyline instance.

### Parameters

#### Global Thickness Setting
You can modify the global thickness multiplier via the `float ThicknessMultiplier` property.

#### Adding a new Vertex
The `Add(...)` method lets you add a new vertex to the NeatPolyline:

```c#
public void Add(Vector2 point, float? thickness = null, Color? color = null)
```
All parameters are optional. If `thickness` or `color` are not specified, NeatPolyline uses the values for the last vertex.
If `point` is not specified, NeatPolyline chooses a place near the last vertex.

#### Removing Vertices
The `RemoveAt(int index)` and `RemoveLast()` methods can be used to remove vertices.

#### Modifying Vertices
You can modify coordinates, thicknesses and colors of vertices via the following methods:
- `SetVector(int index, Vector2 value)`
- `SetThickness(int index, Vector2 value)`
- `SetColor(int index, Vector2 value)`

#### Low-level Access
Although not recommended, you can use the `_points`, `_colors` and `_thicknesses` arrays to directly manipulate values for each index. If you modify a value, make sure to call `MakeDirty()` afterwards to update the mesh.

Do not change the size of these arrays.