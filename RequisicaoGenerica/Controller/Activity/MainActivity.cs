using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using static RequisicaoGenerica.Controller.Helper.ServicesRequest;
using Android.Content;
using Java.IO;

namespace RequisicaoGenerica.Controller.Model
{
    [Activity(Label = "RequisicaoGenerica", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText EtCep;
        Button BtnConsultar;
        TextView TvCep, TvLogradouro, TvComplemento, TvBairro, TvLocalidade, TvUF;

        private const string CepInvalido = "Ops... CEP inválido";

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
        }

        private async void ConsultarCEP()
        {
            if (EtCep.Length() != 8)
            {
                Toast.MakeText(this, CepInvalido, ToastLength.Short).Show();
            }
            else
            {
                await RequisicaoCEP();
            }
        }

        public async Task RequisicaoCEP()
        {
            var cepService = new CepService<ModelCEP>(EtCep.Text.ToString());
            var retornoRequisicao = await cepService.Consultar(null, null);
        }
    }
}

