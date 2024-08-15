namespace TheatherPlayersInfra;
public interface IUnityOfWork
{
    Task Commit();
}
