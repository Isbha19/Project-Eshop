

namespace Eshop.Utility
{
    public static class SD
    {
        //roles
        public const string Role_Customer = "Customer";
        public const string Role_Admin = "Admin";
        //order status
        public const string statusPending= "Pending";
		public const string statusApproved = "Approved";
        public const string statuspartiallyReturned = "Partially Returned";
        public const string statusReturned = "Returned";

        public const string statusProcessing = "Processing";
		public const string statusShipped = "Shipped";
        public const string statusDelivered = "Delivered";
        public const string statusCancelled = "Cancelled";

        //payment type
        public const string cashOnDelivery = "cash";
        public const string onlinePayment = "card";

        public const string walletPayment = "wallet";
        public const string PaymentStatusPending = "Pending";
		public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRefunded = "Refunded";
		public const string PaymentStatusClosed = "Payment Closed";

		public const string PaymentStatusPartialRefunded = "Partially Refunded";


        public const string PaymentStatusRejected = "Rejected";


        public const string Updated = "Updated Successfully";
        public const string Created = "Created Successfully";
        public const string Deleted = "Deleted Successfully";
		public const string TransactionCancelRefund = "Cancel Refund Transaction";
		public const string TransactionReturnRefund = "Return Refund Transaction";

		public const string TransactionWithDraw = "Withdraw Transaction";

        public const string RefferalFirstOrder = "HELLO50";
        public const string ProductOffer = "Product Offer";
        public const string CategoryOffer = "Category Offer";

        //shipping charge
        public const int ShippingCharge = 40;
        public const int freeShippingAmount = 300;


    


    }
}
