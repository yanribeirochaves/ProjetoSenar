using System;
using System.Collections.Generic;
using System.Text;
using SenarDigital.Classes;

namespace SenarDigital.Classes
{
    class Curso
    {
		public string nome { get; set; }
		public string sindicato { get; set; }
		public int quantidadeAulas { get; set; }
		public string codigo { get; set; }
		public List<Aluno> listaDeAlunos { get; set; }
		
		public Curso()
		{
			listaDeAlunos = new List<Aluno>();
		}
	}
}
