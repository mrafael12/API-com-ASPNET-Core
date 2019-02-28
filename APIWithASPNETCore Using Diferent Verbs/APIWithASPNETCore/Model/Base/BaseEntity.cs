using System.ComponentModel.DataAnnotations.Schema;

namespace APIWithASPNETCore.Model.Base
{
    //[DataContract]    
    public class BaseEntity
    {
        [Column("id")]
        public long? Id { get; set; }
    }
}
