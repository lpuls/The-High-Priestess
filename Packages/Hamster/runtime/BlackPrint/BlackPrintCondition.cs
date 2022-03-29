
namespace Hamster.BP {
    [System.Serializable]
    public class BlackPrintCondition : BlackPrintActionBase, IBlackPrintCondition {

        public virtual bool CheckCondition() {
            return false;
        }
    }
}