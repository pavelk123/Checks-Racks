using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checks_Racks.Models
{

    public class Line:Data
    {

        public int Id { get; set; }

        public int ApiId { get; set; }
        public string Name { get; set; }


        public virtual ICollection<Computer> Computers { get; set; }
    }
}
