namespace TripEBuy.Common
{
    /// <summary>
    /// Summary description for DetailInfoA
    /// </summary>
    public class DetailInfoA
    {
        public DetailInfoA()
        {
            //
            // TODO: Add constructor logic here
            //
            this.actionResult = actionType.notAction;
            this.actionMessage = "not action";
        }

        /// <summary>
        /// action result type
        /// </summary>
        public enum actionType
        {
            successful = 1,
            notAction = 0,
            faild = -1
        }

        public actionType actionResult;
        public string actionMessage;
    }
}