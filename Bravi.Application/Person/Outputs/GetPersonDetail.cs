
namespace Bravi.Application.Person.Outputs
{
    public class GetPersonDetail
    {
        public string Nome { get; set; }
        public DateTime Aniversario { get; set; }
        public DateTime CriadoEm { get; set; }
        public IEnumerable<GetContactDetail> Contatos {get; set; }
    }
}
