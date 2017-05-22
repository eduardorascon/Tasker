using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataServices.HELPDESK
{
    public class HelpDeskServices
    {

        public IList<Object> GetOpenRequests()
        {
            string APIKey = "D16B56C3-55A5-4EF0-A4D1-CB9F199865C7";
            var client = new HttpClient();
            string parameters = string.Empty;
            client.BaseAddress = new Uri("DockerHelpDesk:8080/");

            XmlDocument doc = new XmlDocument();
            parameters = "TECHNICIAN_KEY=" + APIKey;
            parameters += "&OPERATION_NAME=GET_REQUESTS";
            doc.LoadXml(@"<Details>
								<parameter>
									<name>from</name>
									<value>0</value>
								</parameter>
								<parameter>
									<name>limit</name>
									<value>50</value>
								</parameter>
								<parameter>
									<name>filterby</name>
									<value>MyOpen_Or_Unassigned</value>
								</parameter>
							</Details>");
            parameters += "&INPUT_DATA=" + doc.OuterXml;

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var apiQuery = "sdpapi/request/";
            HttpResponseMessage response = client.PostAsync(apiQuery, new StringContent(parameters, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;

            IList<object> Requests = new ObservableCollection<object>();

            if (response.IsSuccessStatusCode)
            {
                string resp = response.Content.ReadAsStringAsync().Result;
                XmlReader reader = XmlReader.Create(new StringReader(resp));

                Requests = GetObjectsCollectionFromXML(reader);
            }
            return Requests;
        }

        public Object GetRequestById(string key)
        {
            string APIKey = "D16B56C3-55A5-4EF0-A4D1-CB9F199865C7";
            var client = new HttpClient();
            string parameters = string.Empty;
            client.BaseAddress = new Uri("DockerHelpDesk:8080/");

            XmlDocument doc = new XmlDocument();
            parameters = "TECHNICIAN_KEY=" + APIKey;
            parameters += "&OPERATION_NAME=GET_REQUEST";
            parameters += "&INPUT_DATA=" + doc.OuterXml;

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var apiQuery = string.Format("sdpapi/request/{0}", key);
            HttpResponseMessage response = client.PostAsync(apiQuery, new StringContent(parameters, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;

            object Request = new object();

            if (response.IsSuccessStatusCode)
            {
                string resp = response.Content.ReadAsStringAsync().Result;
                XmlReader reader = XmlReader.Create(new StringReader(resp));
                Request = GetObjectFromXML(reader);
            }

            return Request;
        }

        public class WorkOrderDTO
        {
            public string WorkOrderid { get; set; }
            public string Requester { get; set; }
            public string Subject { get; set; }
            public string Error { get; set; }
        }
        
        public IList<Object> GetObjectsCollectionFromXML(XmlReader reader)
        {

            string porpertyString = string.Empty;
            reader.MoveToAttribute("Details");
            ObservableCollection<Object> Requests = new ObservableCollection<Object>();

            while (reader.Read())
            {
                if (reader.Name == "Details")
                {
                    while (reader.Read() && reader.Name != "Details")
                    {

                        if (reader.Name == "record")
                        {
                            IDictionary<string, object> expando = new ExpandoObject();
                            while (reader.Read() && reader.Name != "record")
                            {
                                porpertyString = string.Empty;
                                if (reader.Name == "parameter")
                                {
                                    while (reader.Read() && (reader.Name != "parameter"))
                                    {
                                        if (reader.NodeType == XmlNodeType.Text) //Display the text in each element.
                                        {
                                            if (string.IsNullOrEmpty(porpertyString))
                                            {
                                                porpertyString = reader.Value;
                                            }
                                            else
                                            {
                                                expando[porpertyString] = reader.Value;
                                                porpertyString = porpertyString + " = " + reader.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            Requests.Add(expando);

                        }

                    }
                }
            }
            return Requests;
        }

        public object GetObjectFromXML(XmlReader reader)
        {

            string porpertyString = string.Empty;
            reader.MoveToAttribute("Details");
            IDictionary<string, object> Request = new ExpandoObject();
            while (reader.Read())
            {
                if (reader.Name == "Details")
                {
                    while (reader.Read() && reader.Name != "Details")
                    {
                        porpertyString = string.Empty;
                        if (reader.Name == "parameter")
                        {
                            while (reader.Read() && (reader.Name != "parameter"))
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    if (string.IsNullOrEmpty(porpertyString))
                                    {
                                        porpertyString = reader.Value;
                                    }
                                    else
                                    {
                                        Request[porpertyString] = reader.Value.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Request;
        }

    }
}
