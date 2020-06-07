# color-hole

# Adjust Camera Angle
![alt text](https://i.ibb.co/5BrtkWy/camera-angle-exp.png)
```
public void AdjustCameraAngle()
{
     float angleX = 90 - camera.transform.eulerAngles.x;
     float a = camera.fieldOfView * 0.5f;
     Bounds bounds = board.GetComponent<MeshRenderer>().bounds;
     Vector3 objectSizes = bounds.max - bounds.min;
     float w = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
     float h = w / camera.aspect;
     float distance1 =  h * 0.5f / Mathf.Tan(a * Mathf.Deg2Rad);
     float distance2 = h * 0.5f / Mathf.Tan((90 - angleX) * Mathf.Deg2Rad);
     float zDistance = h * 0.5f / Mathf.Sin((90 - angleX) * Mathf.Deg2Rad);
     Vector3 vecMid = bounds.center;
     vecMid.z += zDistance;
     camera.transform.position = vecMid - ((distance1 + distance2) * camera.transform.forward);
}
```
# Result : 
![alt text](https://i.ibb.co/wNQfL0L/ref.png)
