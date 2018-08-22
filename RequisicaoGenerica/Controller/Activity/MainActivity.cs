using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Views.InputMethods;
using static RequisicaoGenerica.Controller.Helper.ServicesRequest;
using Android.Views;

namespace RequisicaoGenerica.Controller.Model
{
    [Activity(Theme = "@style/MyCustomTheme", Label = "RequisicaoGenerica", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText EtCep;
        Button BtnConsultar;
        TextView TvCep, TvLogradouro, TvComplemento, TvBairro, TvLocalidade, TvUF;
        ProgressBar ProgressBar;

        private const string FormatoInvalido = "Ops... Formato do CEP inválido";
        private const string CepInvalido = "CEP inválido, verifique se o CEP está correto e tente novamente";
        private const string OcorreuErro = "Erro, verifique sua conexão e tente novamente";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            Init();

            BtnConsultar.Click += delegate
            {
                ConsultarCEP();
            };
        }

        private void Init()
        {
            EtCep = (EditText)FindViewById(Resource.Id.etCEP);
            BtnConsultar = (Button)FindViewById(Resource.Id.btnConsultar);

            TvCep = (TextView)FindViewById(Resource.Id.cep);
            TvLogradouro = (TextView)FindViewById(Resource.Id.logradouro);
            TvComplemento = (TextView)FindViewById(Resource.Id.complemento);
            TvBairro = (TextView)FindViewById(Resource.Id.bairro);
            TvLocalidade = (TextView)FindViewById(Resource.Id.localidade);
            TvUF = (TextView)FindViewById(Resource.Id.uf);

            ProgressBar = (ProgressBar)FindViewById(Resource.Id.progressBar);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(EtCep.WindowToken, 0);
            return base.OnTouchEvent(e);
        }

        private async void ConsultarCEP()
        {
            if (EtCep.Length() != 8)
            {
                Toast.MakeText(this, FormatoInvalido, ToastLength.Short).Show();
            }
            else
            {
                await RequisicaoCEP();
            }
        }

        public async Task RequisicaoCEP()
        {
            ProgressBar.Visibility = ViewStates.Visible;

            var cepService = new CepService<ModelCEP>(EtCep.Text.ToString());
            var retornoRequisicao = await cepService.Consultar(null, null);
            if (retornoRequisicao != null)
            {
                if (retornoRequisicao.Erro)
                {
                    ProgressBar.Visibility = ViewStates.Gone;
                    Toast.MakeText(this, CepInvalido, ToastLength.Short).Show();
                }
                else
                {
                    ProgressBar.Visibility = ViewStates.Gone;
                    PopularDados(retornoRequisicao);
                }
            }
            else
            {
                ProgressBar.Visibility = ViewStates.Gone;
                Toast.MakeText(this, OcorreuErro, ToastLength.Short).Show();
            }
        }

        private void PopularDados(ModelCEP retornoRequisicao)
        {
            TvCep.Text = "CEP: " + retornoRequisicao.Cep;
            TvLogradouro.Text = "Logradouro: " + retornoRequisicao.Logradouro;
            TvComplemento.Text = "Complemento: " + retornoRequisicao.Complemento;
            TvBairro.Text = "Bairro: " + retornoRequisicao.Bairro;
            TvLocalidade.Text = "Localidade: " + retornoRequisicao.Localidade;
            TvUF.Text = "UF: " + retornoRequisicao.Uf;

            TvCep.Visibility = Android.Views.ViewStates.Visible;
            TvLogradouro.Visibility = Android.Views.ViewStates.Visible;
            TvComplemento.Visibility = Android.Views.ViewStates.Visible;
            TvBairro.Visibility = Android.Views.ViewStates.Visible;
            TvLocalidade.Visibility = Android.Views.ViewStates.Visible;
            TvUF.Visibility = Android.Views.ViewStates.Visible;
        }
    }
}

