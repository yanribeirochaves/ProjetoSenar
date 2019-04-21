using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SenarDigital.Classes;

namespace SenarDigital.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdicionarCurso : ContentPage
	{
		//variáveis globais que serão utilizadas nos métodos
		Curso cursoAtual;

		//variavel que diz se o curso é um curso que ainda n existe ou se ele será editado
		Boolean editar = false;
		//variavel que diz se o curso foi cadastrado
		Boolean cadastro = false;

		public AdicionarCurso()
		{
			InitializeComponent();

			//inicia com a tela de cursos padrão
			StackListaVazia.IsVisible = false;
			StackListaPreenchida.IsVisible = false;
			cursoAtual = new Curso();

		}

		//metodo sobrescrito para atualizar a lista de alunos
		protected override void OnAppearing()
		{
			//caso seja um curso iniciado, lista os alunos
			if (CursoPreenchido.IsVisible == true)
			{
				ListaDeAlunos.ItemsSource = null;

				//acha o curso no bd do aparelho e lista os alunos daquele curso
				for(int i = 0; i < new GerenciadorCurso().Listagem().Count; i++)
				{
					if (cursoAtual.codigo == new GerenciadorCurso().Listagem()[i].codigo)
					{
						ListaDeAlunos.ItemsSource = new GerenciadorCurso().Listagem()[i].listaDeAlunos;
					}
				}
				
				//inverte os componentes que serão apresentados, mostrando penas a lista de alunos
				StackListaVazia.IsVisible = false;
				StackListaPreenchida.IsVisible = true;
			}
			
		}

		protected override bool OnBackButtonPressed()
		{
			App.Current.MainPage = new NavigationPage(new Projetos());
			return true;
		}

		private void AdicionarCursos(object sender, EventArgs args)
		{
			//objeto antigo para acha-lo na lista de cursos e trocar pelos novos dados
			Curso cursoNovo = new Curso();

			//estrutura de decisão para saber se será cadastrado um curso novo ou se será um curso editado
			if (!editar) //cadastro do curso novo
			{
				// objeto sendo 'reiniciado' para não haver problemas memoria de variavel
				cursoAtual = new Curso();
				// objeto curso recebendo o texto da caixa de texto
				cursoAtual.nome = EntradaNomeCurso.SelectedItem.ToString();
				cursoAtual.codigo = EntradaCodigoCurso.Text;
                cursoAtual.sindicato = EntradaSindicatoCurso.SelectedItem.ToString();
				cursoAtual.quantidadeAulas = Convert.ToInt32(EntradaQuantDiasCurso.Text);

				cadastro = new GerenciadorCurso().Salvar(cursoAtual);
			}
			else //edição do curso existente
			{
				// objeto curso recebendo o texto da caixa de texto
				cursoNovo.nome = EntradaNomeCurso.SelectedItem.ToString();
				cursoNovo.codigo = EntradaCodigoCurso.Text;
				cursoNovo.sindicato = EntradaSindicatoCurso.SelectedItem.ToString();
				cursoNovo.quantidadeAulas = Convert.ToInt32(EntradaQuantDiasCurso.Text);

				cadastro = new GerenciadorCurso().Editar(cursoAtual, cursoNovo);
			}

			//Se o cadastro for true, então o menu do curso é preenchido
			if (cadastro == true)
			{
				if (editar)
				{
					editar = false;
					cursoAtual = cursoNovo;
				}
				else //caso seja um curso novo
				{
					//botao para cadastrar alunos aparece com menssagem
					StackListaVazia.IsVisible = true;
				}
				//Preenchendo as labels de nome e código com o curso que foi cadastrado
				NomeCursoCadastrado.Text = cursoAtual.nome;
				CodigoCursoCadastrado.Text = cursoAtual.codigo;

				//Tira da tela os elementos de entrada dos dados de nome e código
				//e coloca na tela o 'menu' do curso, com editar e remover.
				//StackLayouts que vao trocar de lugar
				CursoVazio.IsVisible = false;
				CursoPreenchido.IsVisible = true;
				
			}
			else
			{
				//mensagem de erro caso o codigo do curso ja exista na base do aparelho
				DisplayAlert("Erro", "O número do projeto já existe em outro Curso","OK");
			}
		}

		private void RemoverCurso(object sender, EventArgs args)
		{
			//remove o curso do banco de dados do aparelho
			new GerenciadorCurso().Remover(cursoAtual);
			
			//volta para a tela inicial
			App.Current.MainPage = new NavigationPage(new Projetos());
		}

		private void EditarCurso(object sender, EventArgs args)
		{
			//Tira da tela os elementos de 'menu' do curso, com editar e remover
			//e coloca na tela os elementos de entrada dos dados de nome e código
			//StackLayouts que vao trocar de lugar
			CursoVazio.IsVisible = true;
			CursoPreenchido.IsVisible = false;

			editar = true;
		}

		//abrir a tela adicionarAluno
		private void AdicionarAluno (object sender, EventArgs args)
		{
			Navigation.PushAsync(new Telas.AdicionarAluno(cursoAtual.codigo));
		}

		private void AddAluno(object sender, EventArgs args)
		{
			Navigation.PushAsync(new Telas.AdicionarAluno(cursoAtual.codigo));
		}

		private void EditarAluno(object sender, EventArgs args)
		{
			DisplayAlert("teste", "hashdj", "ok");
		}

		private void RemoverAluno(object sender, EventArgs args)
		{
			DisplayAlert("teste", "hashdj", "ok");
		}

		private void SelecaoAluno (object sender, EventArgs args)
		{
			/*Image btnEditar = new Image();
			if(Device.RuntimePlatform == Device.UWP)
			{
				btnEditar.Source = ImageSource.FromFile("Resources/buttonEditar.png");
			}
			else
			{
				btnEditar.Source = ImageSource.FromFile("buttonEditar.png");
			}
			btnEditar.WidthRequest = 100;
			btnEditar.HeightRequest = 100;*/
			
		}
	}
}