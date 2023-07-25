using NSubstitute;
using NUnit.Framework;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewerTests
{
    [TestFixture]
    public class FileImporterTests
    {
        [Test]
       public void OpenDialog()
        {
            var jsonFileProcessor = Substitute.For<IJsonFileProcessor>();
        }
    }
}