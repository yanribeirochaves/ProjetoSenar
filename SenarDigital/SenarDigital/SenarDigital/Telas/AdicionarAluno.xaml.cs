using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SenarDigital.Classes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SenarDigital.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdicionarAluno : ContentPage
	{
		Aluno aluno;
		string codigoCurso = "";
		public AdicionarAluno (string codigoCurso)
		{
			InitializeComponent ();
			aluno = new Aluno();
			this.codigoCurso = codigoCurso;
		}

		private void Enviar(object sender, EventArgs args)
		{
			aluno.nome = NomeAluno.Text;
			aluno.cpf = CPFAluno.Text;

			//criar mensagem caso de erro o cadastro
			Boolean cadastro = new GerenciadorCurso().CadastrarAluno(aluno, codigoCurso);

			if (cadastro)
			{
				//volta para a tela anterior
				Navigation.PopAsync();
			}
			else
			{
				DisplayAlert("Erro", "Aluno não cadastrado", "Ok");
			}
			
		}
	}
}