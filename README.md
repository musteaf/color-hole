# color-hole
That is a copy of the Color Hole game which produced in 5 days.
You can find the apk.
## Reduced batch size
- One for hole and shapes's material
- One for mask
- One for board
- One for canvas
- One for text
<br/>
some screen sizes it rising to 6 but it is about the second board so we can extend the way between first and second board to preserve batchsize.
<br/>
![alt text](https://i.ibb.co/rMFmFz1/dynamicbatch.png)
## Similar Physics with Original Game
## Generic Game
we can and add new shapes, also we can easly create limitless boards like bonus levels.
## All objects use same texture
we can easly adjust colors with changing offset of materials 
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
