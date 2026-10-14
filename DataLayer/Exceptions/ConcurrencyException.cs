namespace DomainLayer.Exceptions
{
    public class ConcurrencyException(Exception ex): Exception(ex.Message, ex);
    
}
