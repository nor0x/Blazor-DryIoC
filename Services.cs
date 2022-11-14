namespace BlazorDryIoC;

public interface IMyService
{
    string GetString();
}


public interface IMyDependentSerivce
{
    string GetString();
}

public interface IBaseInterface
{
    string GetString();
}

public class MyService : IMyService
{
    public string GetString()
    {
        return DateTime.Now.ToString();
    }
}

public class MyDependentService : IMyDependentSerivce
{
    private IMyService _myService => _myLazyService.Value;
    private readonly Lazy<IMyService> _myLazyService;

    public MyDependentService(Lazy<IMyService> myService)
    {
        _myLazyService = myService;
    }


    public string GetString()
    {
        return _myService.GetString();
    }
}

public class FirstService : IBaseInterface
{
    public string GetString()
    {
        return "FirstService";
    }
}

public class SecondService : IBaseInterface
{
    public string GetString()
    {
        return "SecondService";
    }
}

public class ThirdService : IBaseInterface
{
    public string GetString()
    {
        return "ThirdService";
    }
}