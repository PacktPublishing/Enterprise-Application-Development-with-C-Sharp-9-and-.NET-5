using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DISampleWeb.Models
{
    public class HomeViewModel
    {
        public int Singleton { get; set; }
        public int Scoped { get; set; }
        public int Scoped2 { get; internal set; }
        public int Transient { get; set; }
        public int Transient2 { get; internal set; }
    }

    public class ViewData
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
