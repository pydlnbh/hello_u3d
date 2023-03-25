using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumTestScene : MonoBehaviour
{
    private IEnumerator _customEnum;

    // Start is called before the first frame update
    void Start()
    {
        _customEnum = DoMoveCube();
        StartCoroutine(_customEnum);
    }

    // Update is called once per frame
    void Update()
    {
        //if (null != _customEnum)
        //{
        //    _customEnum.MoveNext();
        //    Debug.Log(_customEnum.Current);
        //}

        // _customEnum?.MoveNext();
    }

    public IEnumerator DoMoveCube()
    {
        yield return 1;

        var tCube = GameObject.Find("Cube").transform;

        do
        {
            while (true)
            {
                if (tCube.position.x > 5)
                {
                    break;
                }

                tCube.Translate(Vector3.right * 5 * Time.deltaTime);
                yield return 1;
            }

            while (true)
            {
                if (tCube.position.y > 5)
                {
                    break;
                }

                tCube.Translate(Vector3.up * 5 * Time.deltaTime);
                yield return 1;
            }

            while (true)
            {
                if (tCube.position.x < 0)
                {
                    break;
                }

                tCube.Translate(Vector3.left * 5 * Time.deltaTime);
                yield return 1;
            }

            while (true)
            {
                if (tCube.position.y < 0)
                {
                    break;
                }

                tCube.Translate(Vector3.down * 5 * Time.deltaTime);
                yield return 1;
            }
        } while(true); 
        
    }
}
