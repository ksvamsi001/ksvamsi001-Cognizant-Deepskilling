public interface IDocument
{
    string FileName { get; }
    DateTime CreatedAt { get; }
    void Open();
    void Save();
    void Print();
    void Close();
}
