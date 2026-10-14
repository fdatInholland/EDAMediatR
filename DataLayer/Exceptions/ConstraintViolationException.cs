namespace DomainLayer.Exceptions
{
    public class ConstraintViolationException(Exception ex): Exception(ex.Message, ex);
    
}
