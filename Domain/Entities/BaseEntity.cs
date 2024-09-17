using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Domain.Entities;

public abstract class BaseEntity
{
    [XmlIgnore]
    [Key]
    public int Id { get; set; }
}