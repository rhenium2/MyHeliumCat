namespace HeliumInsighter.Responses;

public class GenericResponse<T>
{
    public T Data { get; set; }
    public string Cursor { get; set; }
}