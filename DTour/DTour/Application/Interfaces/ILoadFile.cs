namespace DTour.Application.Interfaces;

public interface ILoadFile: IManager
{
    public T LoadFileAsync<T>(string filePath);
}