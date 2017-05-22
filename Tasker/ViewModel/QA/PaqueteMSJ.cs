using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.ViewModel.QA
{
   public class PaqueteMSJ
    {
        /// <summary>
        /// Clase que sirve de mensajero para almacenar los cambios que ocurren en el View y el viewModel.
        /// </summary>
        public string NombrePropiedad { get; set; }
        public string Informacion { get; set; }
    }
}
