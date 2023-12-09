using System.ComponentModel.DataAnnotations;

namespace Internet_Market_WebApi.Data.Entities
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
