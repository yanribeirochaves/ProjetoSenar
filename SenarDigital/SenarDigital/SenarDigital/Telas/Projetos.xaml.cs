using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SenarDigital.Telas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SenarDigital.Classes;

namespace SenarDigital.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Projetos : ContentPage
	{
		public Projetos ()
		{
			InitializeComponent ();

			//atualizando lista de cursos testes
			ListaDeCurso.ItemsSource = null;
			ListaDeCurso.ItemsSource = new GerenciadorCurso().Listagem();

			//se a lista estiver cheia, apresenta na tela a lista de cursos
			//se estiver vazia, apresenta a label de 'zero cursos' e o botao de inciar
			if (new GerenciadorCurso().Listagem().Count() > 0)
			{
				ListaCursosStack.IsVisible = true;
				ListaVazia.IsVisible = false;
			}
			else
			{
				ListaCursosStack.IsVisible = false;
				ListaVazia.IsVisible = true;
			}
		}

		//abre a tela de adicionar curso empilhada, ou seja, a tela de projetos permanece em ativa em segundo plano
		private void IniciarCurso(object sender, EventArgs args)
		{
			Navigation.PushAsync(new Telas.AdicionarCurso());
		}

		//abre a tela de adicionar curso empilhada, ou seja, a tela de projetos permanece em ativa em segundo plano
		private void IniciarMais(object sender, EventArgs args)
		{
			Navigation.PushAsync(new Telas.AdicionarCurso());
		}
	}
}