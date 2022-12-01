using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientArray : MonoBehaviour
{
    [SerializeField] int x_1 = 15;
    [SerializeField] int y_1 = 15;
    [SerializeField] int z_1 = 15;
    [SerializeField] int sphereDistance = 15;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] float gradientStep = 0.1f;
    [SerializeField] Gradient albedoTint;
    [SerializeField] AnimationCurve colourCurve;
    [SerializeField] float duration = 5f;

    MaterialPropertyBlock propertyBlock;
    float _currentTime = 0f;
    GameObject[,,] spheres;
    float[,,] timeArray;
    int x, y, z = 0;
    bool created = false;
    void Awake()
    {
        spheres = new GameObject[x_1, y_1, z_1];
        timeArray = new float[x_1, y_1, z_1];
        propertyBlock = new MaterialPropertyBlock();
        CreateSphereCube();
    }
    /// <summary>
    /// Create Sphere Cube adjust x_1 y_1 z_1 values for sphere size
    /// gradientStep for gradient change speed
    /// </summary>
    void CreateSphereCube()
    {
        float createTime = 0;
        for (int i = 0; i < x_1; i++)
        {
            x += sphereDistance;
            for (int j = 0; j < y_1; j++)
            {
                y += sphereDistance;
                for (int k = 0; k < z_1; k++)
                {
                    z += sphereDistance;
                    spheres[i, j, k] = Instantiate(spherePrefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                    timeArray[i, j, k] = createTime;
                }
                z = 0;
            }
            y = 0;
            createTime += gradientStep;
        }
        created = true;
    }
    void Update()
    {
        if (created)
            for (int i = 0; i < x_1; i++)
            {
                for (int j = 0; j < y_1; j++)
                {
                    for (int k = 0; k < z_1; k++)
                    {
                        UpdateGradient(spheres[i, j, k],i,j,k);
                    }
                }
            }
    }
    /// <summary>
    /// Call for sphere gradient update, get time from object and update with new time
    /// Same with color get new color and set it
    /// </summary>
    /// <param name="spher"></param>
    /// passed gradient object
    void UpdateGradient(GameObject spher, int i,int j, int k )
    {
        //set time
        _currentTime = timeArray[i, j, k];
        timeArray[i, j, k] = Mathf.Repeat(_currentTime + Time.deltaTime, duration);
        // retrieve and apply the colour
        var colourProgress = colourCurve.Evaluate(_currentTime / duration);
        var newColour = albedoTint.Evaluate(colourProgress);
        propertyBlock.SetColor("_Color", newColour);
        spher.GetComponent<MeshRenderer>().SetPropertyBlock(propertyBlock);
    }
}
