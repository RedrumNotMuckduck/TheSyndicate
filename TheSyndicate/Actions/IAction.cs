namespace TheSyndicate.Actions
{
    public interface IAction
    {
        public void ExecuteAction();
        public int GetIndexOfDestinationBasedOnUserSuccessOrFail();
        public bool DidPlayerSucceed();
    }
}
