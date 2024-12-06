using System;

namespace TaskAPI.Controllers;
using TaskAPI.Models;

public class AppDBContext
{
    public static List<MyTask> _taskList = new List<MyTask>{
        new MyTask{Id=1,Title="Code",Description="Code sign-up logic",IsCompleted=false,CreatedAt=DateTime.Now}
    };
}
