using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using CountingApp.Repositories;

namespace CountingApp
{
    [Activity(Label = "Sessions", MainLauncher = true)]
    public class SessionsActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            App.Initialize();

            var data = App.Container.Resolve<ISessionsRepository>().GetSessions().Select(x => x.CreatedDateTime.ToString(CultureInfo.InvariantCulture)).ToArray();
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, data);

            SetContentView(Resource.Layout.SessionsActivity);
            // Create your application here
        }
    }
}