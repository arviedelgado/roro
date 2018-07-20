using Microsoft.SharePoint.Client;

namespace Roro.Activities.SharePoint
{
    class CreateListItem : ProcessNodeActivity
    {
        public Input<Text> SiteUrl { get; set; }

        public Input<Text> ListTitle { get; set; }

        public Input<Text> ItemTitle { get; set; }

        public Input<Text> FilePath { get; set; }

        public Input<TrueFalse> Overwrite { get; set; }

        public override void Execute(ActivityContext context)
        {
            //TODO: Handle fields other than the Title field.
            ClientContext clientContext = new ClientContext(context.Get(this.SiteUrl));

            List list = clientContext.Web.Lists.GetByTitle(context.Get(this.ListTitle));

            bool overwrite = context.Get(this.Overwrite, true);

            FileCreationInformation fci = new FileCreationInformation();
            fci.Content = System.IO.File.ReadAllBytes(context.Get(FilePath));
            fci.Overwrite = overwrite;

            clientContext.ExecuteQuery();
        }
    }
}
