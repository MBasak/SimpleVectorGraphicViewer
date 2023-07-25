namespace VectorGraphicViewer.Contracts
{
    /// <summary>
    /// Wrapper over Open File Dialog to
    /// files using modal dialogs unit testable
    /// </summary>
    public interface IFileDialog
    {
        bool? OpenFileDialog(string fileExtensions, out string filename);
    }
}