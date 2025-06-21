public class WordDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument() => new WordDocument();
}
