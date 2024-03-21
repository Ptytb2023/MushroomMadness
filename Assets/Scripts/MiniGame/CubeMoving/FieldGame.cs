using MiniGame.CubesMoving;
using System.Collections.Generic;
using UnityEngine;

public class FieldGame : MonoBehaviour
{
    [SerializeField] private ContenerCubs _contener;

    private Dictionary<Transform, Vector3> _cubesAndStartPosition;

    private void Start()
    {
        _cubesAndStartPosition = new Dictionary<Transform, Vector3>();

        foreach (var cube in _contener.GetCubes())
        {
            _cubesAndStartPosition.Add(cube.transform, cube.transform.position);
        }
    }

    public void ResetField()
    {
        foreach (var cube in _cubesAndStartPosition.Keys)
        {
            cube.position = _cubesAndStartPosition[cube];
        }
    }
}
