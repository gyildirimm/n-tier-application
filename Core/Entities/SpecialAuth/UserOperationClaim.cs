namespace Core.Entities.SpecialAuth
{
    public class UserOperationClaim : BaseEntity<int>
    {
        public int UserId { get; set; }

        public int OperationClaimId { get; set; }
    }
}
