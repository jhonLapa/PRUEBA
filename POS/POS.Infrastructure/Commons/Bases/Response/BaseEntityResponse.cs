namespace POS.Infrastructure.Commons.Bases.Response
{
    public class BaseEntityResponse<T>
    {
        public List<T>? Items { get; set; }
        public int TotalRecords { get; set; }
    }
}
