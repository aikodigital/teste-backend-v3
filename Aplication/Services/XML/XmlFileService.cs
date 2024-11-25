using System.Xml.Linq;

namespace Aplication.Services.XML
{
    public class XmlFileService
    {
        private readonly string _savePath;

        public XmlFileService(string savePath)
        => _savePath = savePath;
        
        public async Task SaveXmlAsync(string xml)
        {
            if (!Directory.Exists(_savePath))
                Directory.CreateDirectory(_savePath);
            
            string fileName = $"{DateTime.Now:yyyy-MM-dd_HH_mm_ss}";
            var filePath = Path.Combine(_savePath, $"{fileName}.xml");
            await File.WriteAllTextAsync(filePath, xml);
        }
    }
}
