namespace RentMe.Models
{
	/// <summary>
	/// The error view model responsible for displaying errors to the user
	/// </summary>
	public class ErrorViewModel
	{
		/// <summary>
		/// Gets or sets the request id
		/// </summary>
		public string RequestId { get; set; }

		/// <summary>
		/// Gets the show request id 
		/// </summary>
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
