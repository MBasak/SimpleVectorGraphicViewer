namespace VectorGraphicViewer.Contracts
{
    public interface IVectorGraphicViewerViewModel
    {
        void AddPolygon(IPolygon polygon);

        void AddCircle(ICircle circle);
        void ClearAllShapes();
        void ScaleShapes();
    }
}