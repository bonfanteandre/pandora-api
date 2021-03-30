namespace Pandora.Core.Entities
{
    public class PaymentMethod : Entity
    {
        public string Name { get; private set; }

        public PaymentMethod(string name)
        {
            SetNewId();
            Name = name;
        }

        public void Update(PaymentMethod paymentMethod)
        {
            Name = paymentMethod.Name;
        }
    }
}