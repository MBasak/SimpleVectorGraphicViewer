namespace VectorGraphicViewer.Contracts
{
    public interface IShape
    {
         string Type { get; set; }
         string BorderColor { get; set; }
          
         string FillColor { get; }

         bool Filled { get; set; }
    }
}