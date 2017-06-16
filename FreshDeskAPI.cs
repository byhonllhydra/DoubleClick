using System;
using System.Net;
using System.Text;
using System.IO;

class FreshDeskAPI
{
	
	/*
	
	When application first starts up, check if there is a stored agent ID. If there is none, get the currently authenticated user and store the ID.
	
	This way, API requests can be cut down so as not having to request for agent ID and having 2 requests per request.
	
	*/
	class RequestMethod
	{
		public static string POST = "POST";
		public static string GET = "GET";
		public static string UPDATE = "UPDATE";
	}
	// FreshDesk Domain
	string FDDomain = "freshdeskdomain";
    string APIKey = "";
	string AuthenticationKey = APIKey + ":X";
	
	// On first app launch request this, and store it for later requests
	string AgentID = "123456789";
	
	// Basic API request path
	string RequestPath = "https://" + FDDomain + ".freshdesk.com/api/v2/";
    
	public static string CurrentAgent()
	{
		string request = 
	}
	
	public static string CreateTimeEntry(string timeSpent, string note, string ticketID)
	{
		// Create the request
		string request = '{"note":"' + note + '", "time_spent":"' + timeSpent + '", "agent_id":' + AgentID + ' }';
		
		// Set the API path
		string apiPath = RequestPath + "tickets/" + ticketID + "/time_entries";
		
		// Send off request and return results
		return RequestAPI(request, apiPath, RequestMethod.POST);
	}
	
	// Needs to be fixed - will only be able to POST not GET
	private static string RequestAPI(string request, string apiPath, string requestMethod)
	{
		HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.RequestPath + apiPath);
		request.ContentType = "application/json";
		request.Method = requestMethod;
		
		// Create byte array out of the request
		byte[] requestBytes = Encoding.UTF8.GetBytes(request);
		request.ContentLength = requestBytes.Length;
		
		// Encode the authentication and set it in the header
		string auth = Convert.ToBase64String(Encoding.Default.GetBytes(AuthenticationKey));
		request.Headers["Authorization"] = "Basic " + auth;
		
		//Get the stream that holds request data by calling the GetRequestStream method. 
        Stream dataStream = request.GetRequestStream(); 
        // Write the data to the request stream. 
        dataStream.Write(byteArray, 0, byteArray.Length); 
        // Close the Stream object. 
        dataStream.Close();
        try
        {
            Console.WriteLine("Submitting Request");
			
			//Send the request to the server by calling GetResponse. 
            WebResponse response = request.GetResponse();
			
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
			
            // Open the stream using a StreamReader for easy access. 
            StreamReader reader = new StreamReader(dataStream);
			
            // Read the content. 
            string Response = reader.ReadToEnd();
			
            //return status code
            Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
			
            //return location header
            Console.WriteLine("Location: {0}", response.Headers["Location"]);
			
            //return the response 
            Console.Out.WriteLine(Response);
			return Reponse;
        }
        catch (WebException ex)
        {
            Console.WriteLine("API Error: Your request is not successful. If you are not able to debug this error properly, mail us at support@freshdesk.com with the follwing X-Request-Id");
            Console.WriteLine("X-Request-Id: {0}", ex.Response.Headers["X-Request-Id"]);
            Console.WriteLine("Error Status Code : {1} {0}", ((HttpWebResponse)ex.Response).StatusCode, (int)((HttpWebResponse)ex.Response).StatusCode);
            using (var stream = ex.Response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {   
              Console.Write("Error Response: ");
              Console.WriteLine(reader.ReadToEnd());
			  
			  return reader.ReadToEnd();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(ex.Message);
			return null;
        }
	}
}