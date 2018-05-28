using Microsoft.SharePoint.Client;

namespace Roro.Activities.SharePoint
{
    class UpdateListItem : ProcessNodeActivity
    {
        public Input<Text> SiteUrl { get; set; }

        public Input<Text> ListTitle { get; set; }

        public Input<Text> ItemId { get; set; }

        public Input<Text> ColumnName { get; set; }

        public Input<Text> ColumnValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            ClientContext clientContext = new ClientContext(context.Get(this.SiteUrl));

            List list = clientContext.Web.Lists.GetByTitle(context.Get(this.ListTitle));

            ListItem item = list.GetItemById(context.Get(this.ItemId));
            clientContext.Load(item);
            clientContext.ExecuteQuery();

            item[context.Get(this.ColumnName)] = context.Get(this.ColumnValue);
            item.Update();

            clientContext.ExecuteQuery();
        }
    }
}
