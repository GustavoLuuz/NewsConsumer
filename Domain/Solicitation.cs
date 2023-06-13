namespace NewsPub.Domain
{
    public class Solicitation
    {
        public Solicitation(int solicitationNumber, string email, string message)
        {
            SolicitationNumber = solicitationNumber;
            Email = email;
            Message = message;
        }
        public int SolicitationNumber { get; set; } 
        public string Email { get; set; }
        public string Message { get; set; }
        
    }
}