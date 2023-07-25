
using NSubstitute;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.ViewModels;

namespace VectorGraphicViewerTests
{
    [TestFixture]
    public class VectorGraphicViewModelTests
    {
        VectorGraphicViewerViewModel vectorGraphicViewerViewModel;
        [SetUp]
        public void Setup()
        {
            var scaler = Substitute.For<IShapeScalerHelper>();
            vectorGraphicViewerViewModel = new VectorGraphicViewerViewModel(scaler);
        }

        [Test]
        public void AddCircleAndPolygons()
        {
            IPolygon polygon = Substitute.For<IPolygon>();
            ICircle circle = Substitute.For<ICircle>();

            Assert.That(vectorGraphicViewerViewModel.Polygons.Count, Is.EqualTo(0));
            Assert.That(vectorGraphicViewerViewModel.Circles.Count, Is.EqualTo(0));

            vectorGraphicViewerViewModel.AddPolygon(polygon);
            vectorGraphicViewerViewModel.AddCircle(circle);

            Assert.That(vectorGraphicViewerViewModel.Polygons.Count, Is.EqualTo(1));
            Assert.That(vectorGraphicViewerViewModel.Circles.Count, Is.EqualTo(1));

        }

        [Test]
        public void RemoveCircleAndPolygons()
        {
            IPolygon polygon = Substitute.For<IPolygon>();
            ICircle circle = Substitute.For<ICircle>();

            vectorGraphicViewerViewModel.AddPolygon(polygon);
            vectorGraphicViewerViewModel.AddCircle(circle);

            Assert.That(vectorGraphicViewerViewModel.Polygons.Count, Is.EqualTo(1));
            Assert.That(vectorGraphicViewerViewModel.Circles.Count, Is.EqualTo(1));

            vectorGraphicViewerViewModel.ClearAllShapes();

            Assert.That(vectorGraphicViewerViewModel.Polygons.Count, Is.EqualTo(0));
            Assert.That(vectorGraphicViewerViewModel.Circles.Count, Is.EqualTo(0));

        }
    }
}
