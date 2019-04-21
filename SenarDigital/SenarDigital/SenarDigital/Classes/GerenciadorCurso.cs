using System;
using System.Collections.Generic;
using System.Text;

namespace SenarDigital.Classes
{
    class GerenciadorCurso
    {
		private List<Curso> listaCursos { get; set; }

		//estancia listando os cursos cadastrados na lista de cursos
		public GerenciadorCurso()
		{
			listaCursos = Listagem();
		}

		//metodo para salvar o curso na memoria interna do celular
		public Boolean Salvar(Curso curso)
		{
			Boolean cadastrado = false;
			//Se a lista não for vazia
			if (listaCursos.Count >= 1)
			{
				//percorre a lista de cursos cadastradas no aparelho
				for (int i = 0; i < listaCursos.Count; i++)
				{
					//compara o codigo do curso cadastrado com o codigo dos cursos existentes.
					if (curso.codigo == listaCursos[i].codigo)
					{
						cadastrado = true;
					}
				}
			}

			//se a comparação permanece falsa, entao o curso nao existe no banco de dados interno do aparelho
			if (!cadastrado)
			{
				listaCursos.Add(curso);
				AtualizarProperties(listaCursos);

				//retorna true se cadastrou o curso
				return true;
			}
			// se for true, curso ja existe
			else
			{
				//retorna false se não cadastrou
				return false;
			}
		}
		public List<Curso> Listagem()
		{
			//Serve para não ter bug para inserir uma tag que ja existe no armazenamento do aparelho
			if (App.Current.Properties.ContainsKey("Cursos"))
			{
				return (List<Curso>)App.Current.Properties["Cursos"];
			}

			return new List<Curso>();
		}
		public void Remover(Curso curso)
		{
			listaCursos.Remove(curso);
			AtualizarProperties(listaCursos);
		}

		public Boolean Editar(Curso cursoAntigo, Curso cursoNovo)
		{
			//variavel para saber se o valor foi editado ou nao
			Boolean editado = false;
			//percorre a lista de cursos cadastradas no aparelho
			for (int i = 0; i < listaCursos.Count; i++)
			{
				//compara o codigo do curso cadastrado com o codigo dos cursos existentes.
				if (cursoAntigo.codigo == listaCursos[i].codigo)
				{
					//atualiza o curso dentro da lista de cursos
					listaCursos[i].codigo = cursoNovo.codigo;
					listaCursos[i].nome = cursoNovo.nome;
					listaCursos[i].sindicato = cursoNovo.sindicato;
					listaCursos[i].quantidadeAulas = cursoNovo.quantidadeAulas;
					editado = true;
				}
			}
			//atualiza a base de dados
			AtualizarProperties(listaCursos);
			return editado;
		}

		//metodo para atualizar a base de dados sempre que um a lista de cursos mudar
		private void AtualizarProperties(List<Curso> listaDeCursos)
		{
			//Serve para não ter bug para inserir uma tag que ja existe no armazenamento do aparelho
			if (App.Current.Properties.ContainsKey("Cursos"))
			{
				//se ja existe, remove para adicionar novamente atualizada posteriormente
				App.Current.Properties.Remove("Cursos");
			}

			//Salva em uma biblioteca local do aparelho
			App.Current.Properties.Add("Cursos", listaDeCursos);
		}

		public Boolean CadastrarAluno(Aluno aluno, string codigoCurso)
		{
			listaCursos = new GerenciadorCurso().Listagem();
			for(int i = 0; i < listaCursos.Count; i++)
			{
				if (codigoCurso == listaCursos[i].codigo)
				{
					listaCursos[i].listaDeAlunos.Add(aluno);
					AtualizarProperties(listaCursos);
					return true;
				}
			}
			return false;
		}
	}
}
