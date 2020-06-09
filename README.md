# color-hole
That is a copy of the Color Hole game which produced in **5** days.
You can find the apk.
## Similar Physics with Original Game
![alt text](https://im6.ezgif.com/tmp/ezgif-6-10771cfe469b.gif)
![alt text](https://im6.ezgif.com/tmp/ezgif-6-80b66f8ea5e3.gif)
![alt text](https://im6.ezgif.com/tmp/ezgif-6-fd6000b8681f.gif)
## Reduced batch size
- One for hole and shapes's material
- One for mask
- One for board
- One for canvas
- One for text <br/><br/>
* Some screen sizes it rising to 6 because the camera angle see the second board. Thus we can extend the way between first and second board to preserve batchsize.
<br/>

![alt text](https://i.ibb.co/rMFmFz1/dynamicbatch.png)

## Without Updating Mesh Rendered! Just Updated Collider's Mesh. <br/>
![alt text](https://i.ibb.co/LNttqyB/collider.png)

## With More Than 30 Classes That Made The Game More Generic
* New shape can be added easily
* Limitless boards like bonus levels can be implemented easily.

## All objects use same texture
* Adjust colors by changing offset
## Adjusted camera angle
![alt text](https://i.ibb.co/5BrtkWy/camera-angle-exp.png)
```
        private Vector3 AdjustCameraAngle(Vector3 refPosition)
        {
            var currentBoard = BoardManager.instance.GetCurrentBoard();
            var meshRenderer = currentBoard.MeshRenderer;
            var bounds = meshRenderer.bounds;
            var objectSizes = bounds.max - bounds.min;
            var w = Mathf.Max(objectSizes.x);
            w *= 4.68f / 5f;
            var angleX = 90 - camera.transform.eulerAngles.x;
            var a = camera.fieldOfView * 0.5f;
            var h = w / camera.aspect;
            var distance1 = h * 0.5f / Mathf.Tan(a * Mathf.Deg2Rad);
            var distance2 = h * 0.5f / Mathf.Tan((90 - angleX) * Mathf.Deg2Rad);
            var zDistance = h * 0.5f / Mathf.Sin((90 - angleX) * Mathf.Deg2Rad);
            var vecMid = refPosition;
            vecMid.z += zDistance;
            return vecMid - ((distance1 + distance2) * camera.transform.forward);
        }
```
Result: fits different screen sizes!
     
![alt text](https://i.ibb.co/8PsqrDr/camera-angle-result.png)
