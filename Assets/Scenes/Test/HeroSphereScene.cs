#define __DEBUG

using System.Collections;
using UnityEngine;


public class HeroSphereScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 预编译
        // #if __DEBUG
        #region 代码折叠
#if _DEBUG
        Debug.Log("Hello");

        var student = new Student()
        {
            Id = 1,
            Name = "anxin",
        };

        // Student student = new()
        // {
        //    Id = 1,
        //    Name = "anxin",
        // };

        // Student student = new Student()
        //{
        //    Id = 1,
        //    Name = "anxin",
        //};

        // student.Id = 1;
        // student.Name = "anxin";

        Debug.LogFormat("id = {0}, name = {1}", student.Id, student.Name);

        // 结构体是值传递的
        // updateStudent(student);
        // ref 结构体必须先初始化
        // out 只定义就可以了
        // updateStudent(ref student);
        updateStudent(out student);
        updateStudent(out var student1);

        Debug.LogFormat("id = {0}, name = {1}", student.Id, student.Name);
        Debug.LogFormat("id = {0}, name = {1}", student1.Id, student1.Name);

        var eventSystem = new EventSystem();
        eventSystem.EventListener += () =>
        {
            Debug.Log("事件 1");
        };

        eventSystem.EventListener += () =>
        {
            Debug.Log("事件 2");
        };

        eventSystem.DoEvent();
    }

    //public void updateStudent(ref Student student)
    //{
    //    student.Id = 2;
    //    student.Name = "lixiang";
    //}

    public void updateStudent(out Student student)
    {
        student = new Student();
        student.Id = 2;
        student.Name = "lixiang";

        

        int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };

        foreach (int i in array)
        {
            Debug.Log(i);
        }

        var _enum = array.GetEnumerator();
        
        for (; _enum.MoveNext();)
        {
            Debug.Log(_enum.Current);
        }
#endif
        #endregion

        var @enum = GetEnumerator();

        //for (; @enum.MoveNext();)
        //{
        //    Debug.Log(@enum.Current);
        //}
        @enum.MoveNext();
        Debug.Log(@enum.Current);
        
        @enum.MoveNext();
        Debug.Log(@enum.Current);
    }

    public IEnumerator GetEnumerator()
    {
        //int[] arr = { 0, 1, 2 };
        //return arr.GetEnumerator();

        // yield return 0;
        // yield return 1;
        // yield return 2;

        for (int i = 0; i <= 2; i++)
        {
            yield return i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
