namespace ProjectCopyServer.Users.Eto;

public class AbstractEto<T>
{
    protected AbstractEto(T data)
    {
        Data = data;
    }

    public T Data { get; set; }

}