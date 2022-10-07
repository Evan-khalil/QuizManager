namespace DAL
{
    public interface ISerializerManager
    {
        bool Serialize(string targetFile);
        void Deserialize(string targetFile);
    }
}
