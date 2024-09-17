using TS.Domain.Enums;

namespace TS.Presentation.ViewModels
{
    public class UpdatePlaysViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ETypePLay Type { get; set; }
        public int Lines { get; set; }
    }
}