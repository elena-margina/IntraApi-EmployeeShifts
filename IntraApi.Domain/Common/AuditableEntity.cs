
namespace IntraApi.Domain.Common
{
    public interface IAuditableEntity
    {
        int UserID { get; set; }
        DateTime DModify { get; set; }
    }
}
