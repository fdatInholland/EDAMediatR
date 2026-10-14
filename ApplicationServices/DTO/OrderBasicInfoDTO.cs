namespace ApplicationServices.DTO
{
    public record OrderBasicInfoDTO(Guid id, string CustomerName)
    {
        public DateTime creationTime;

        public OrderBasicInfoDTO(DateTime creationTime) : this(Guid.Empty, string.Empty)
        {
            this.creationTime = creationTime;
        }
    }
}