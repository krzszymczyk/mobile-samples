using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MWC.BL;
using MWC;

namespace MWC.Android.Screens
{
    [Activity(Label = "Speakers")]
    public class SpeakersScreen : BaseScreen
    {
        protected MWC.Adapters.SpeakerListAdapter _speakerList;
        protected IList<Speaker> _speakers;
        protected ListView _speakerListView = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set our layout to be the home screen
            this.SetContentView(Resource.Layout.SpeakersScreen);

            //Find our controls
            this._speakerListView = FindViewById<ListView>(Resource.Id.SpeakerList);

            // wire up task click handler
            if (this._speakerListView != null)
            {
                this._speakerListView.ItemClick += (object sender, ItemEventArgs e) =>
                {
                    var speakerDetails = new Intent(this, typeof(SpeakerDetailsScreen));
                    speakerDetails.PutExtra("SpeakerID", this._speakers[e.Position].ID);
                    this.StartActivity(speakerDetails);
                };
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            this._speakers = MWC.BL.Managers.SpeakerManager.GetSpeakers();

            // create our adapter
            this._speakerList = new MWC.Adapters.SpeakerListAdapter(this, this._speakers);

            //Hook up our adapter to our ListView
            this._speakerListView.Adapter = this._speakerList;
        }
    }
}