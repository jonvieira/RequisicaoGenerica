using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using RequisicaoGenerica.Controller.Model;
using static RequisicaoGenerica.Controller.ServicesRequest;

namespace RequisicaoGenerica
{
    [Activity(Label = "RequisicaoGenerica", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText EtCep;
        Button BtnConsultar;
        private const string CepInvalido = "Ops... CEP inválido";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            EtCep = (EditText)FindViewById(Resource.Id.etCEP);
            BtnConsultar = (Button)FindViewById(Resource.Id.btnConsultar);

            BtnConsultar.Click += delegate
            {
                ConsultarCEP();
            };
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

            Toast.MakeText(this, retornoRequisicao.Localidade, ToastLength.Long).Show();
        }
    }
}

